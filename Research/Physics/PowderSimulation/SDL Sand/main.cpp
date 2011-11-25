#ifdef WIN32
#pragma comment(lib, "../SDL/lib/SDL.lib")
#endif

#include <tchar.h>
#include <cstdlib>
#include <ctime>

#include "../SDL/includes/SDL.h"
#include "math.h"

#include "quantums.h"
#include "render.h"
#include "input.h"

int _tmain(int argc, _TCHAR* argv[])
{
	init();
	Clear();

	int tick = 0;
	int done=0;

	//To emit or not to emit
	bool emitSand = true;
	bool emitWater = true;
	bool emitOil = true;

	//Initial density of emitters
	float oilDens = 0.3f;
	float sandDens = 0.3f;
	float waterDens = 0.3f;

	// Set initial seed
	srand( (unsigned)time( NULL ) );

	int oldx = 0, oldy = 0;

	//Mouse button pressed down?
	bool down = false;

	//Used for calculating the average FPS from NumFrames
	const int NumFrames = 20;
	int AvgFrameTimes[NumFrames];
	for( int i = 0; i < NumFrames; i++)
		AvgFrameTimes[i] = 0;
	int FrameTime = 0;
	int PrevFrameTime = 0;
	int Index = 0;

	//The game loop
	while(done == 0)
	{
		tick++;

		SDL_Event event;
		//Polling events
		while ( SDL_PollEvent(&event) )
		{
			if ( event.type == SDL_QUIT )  {  done = 1;  }

			//Key strokes
			if ( event.type == SDL_KEYDOWN )
			{
				switch (event.key.keysym.sym)
				{
				case SDLK_ESCAPE: //Exit
					done = 1;
					break;
				case SDLK_0: // Eraser
					CurrentParticleType = NOTHING;
					break;
				case SDLK_1: // Draw walls
					CurrentParticleType = WALL;
					break;
				case SDLK_2: // Draw sand		
					CurrentParticleType = SAND;
					break;
				case SDLK_3: // Draw water		
					CurrentParticleType = WATER;
					break;
				case SDLK_4: // Draw oil		
					CurrentParticleType = OIL;
					break;
				case SDLK_UP: // Increase pen size
					penSize *= 2;
					break;
				case SDLK_DOWN: // Decrease pen size
					penSize /= 2;
					if(penSize < 1)
						penSize = 1;
					break;
				case SDLK_DELETE: // Clear screen
					Clear();
					break;
				case SDLK_z: //Enable or disable sand emitter
					emitSand ^= true;
					break;
				case SDLK_x: //Enable or disable water emitter
					emitWater ^= true;
					break;
				case SDLK_q: // Increase sand emitter density
					sandDens += 0.05f;
					if(sandDens > 1.0f)
						sandDens = 1.00f;
					break;
				case SDLK_a: // Decrease sand emitter density
					sandDens -= 0.05f;
					if(sandDens < 0.05f)
						sandDens = 0.05f;
					break;
				case SDLK_w: // Increase water emitter density
					waterDens += 0.05f;
					if(waterDens > 1.0f)
						waterDens = 1.0f;
					break;
				case SDLK_s: // Decrease water emitter density
					waterDens -= 0.05f;
					if(waterDens < 0.05f)
						waterDens = 0.05f;
					break;
				case SDLK_e: // Increase oil emitter density
					oilDens += 0.05f;
					if(oilDens > 1.0f)
						oilDens = 1.0f;
					break;
				case SDLK_d: // Decrease oil emitter density
					oilDens -= 0.05f;
					if(oilDens < 0.05f)
						oilDens = 0.05f;
					break;
				case SDLK_c: //Enable or disable oil emitter
					emitOil ^= true;
					break;
				case SDLK_r: // Draw a bunch of random lines
					DoRandomLines(WALL);
					break;
				case SDLK_t: // Erase a bunch of random lines
					DoRandomLines(NOTHING);
					break;
				case SDLK_o: // Enable or disable particle swaps
					implementParticleSwaps ^= true;
					break;
				}
			}
			// If mouse button pressed then save position of cursor
			if( event.type == SDL_MOUSEBUTTONDOWN)
			{
				SDL_MouseButtonEvent mbe = (SDL_MouseButtonEvent) event.button;
				oldx = mbe.x; oldy=mbe.y;
				DrawLine(mbe.x,mbe.y,oldx,oldy);
				down = true;
			}
			// Button released
			if(event.type == SDL_MOUSEBUTTONUP)
			{
				SDL_MouseButtonEvent mbe = (SDL_MouseButtonEvent) event.button;
				DrawLine(mbe.x,mbe.y,oldx,oldy);
				down = false;
			}
			// Mouse has moved
			if(event.type == SDL_MOUSEMOTION)
			{
				SDL_MouseMotionEvent mme = (SDL_MouseMotionEvent) event.motion;
				if(mme.state & SDL_BUTTON(1))
					DrawLine(mme.x,mme.y,oldx,oldy);
				oldx = mme.x; oldy=mme.y;
			}
		}

		//To emit or not to emit
		if(emitSand)
			Emit(FIELD_WIDTH/4, 20, SAND, sandDens);
		if(emitWater)
			Emit(FIELD_WIDTH/4*2, 20, WATER, waterDens);
		if(emitOil)
			Emit(FIELD_WIDTH/4*3, 20, OIL, oilDens);

		//If the button is pressed (and no event has occured since last frame due
		// to the polling procedure, then draw at the position (enabeling 'dynamic emitters')
		if(down)
			DrawLine(oldx,oldy,oldx,oldy);

		//Clear bottom line
		for (int i=0; i< FIELD_WIDTH; i++) vs[i+((FIELD_HEIGHT-1)*FIELD_WIDTH)] = NOTHING;

		// Update the virtual screen (performing particle logic)
		UpdateVirtualScreen();
		// Map the virtual screen to the real screen
		DrawScene();
		//Fip the vs
		SDL_Flip(screen);

		//Printing out the framerate and particlecount
		FrameTime = SDL_GetTicks();
		AvgFrameTimes[Index] = FrameTime - PrevFrameTime;
		Index = (Index + 1) % NumFrames;
		PrevFrameTime = FrameTime;
		//We'll print for each 50 frames
		if(tick % 50 == 0)
		{
			int avg = 0;
			//Calculating the average over NumFrames frames
			for( int i = 0; i < NumFrames; i++)
				avg += AvgFrameTimes[i];

			avg = 1000/((int)avg/NumFrames);

			printf("FPS: %i\n",avg);
			printf("Particle count: %i\n",renderedParticlesCount);
		}
	}

	//Loop ended - quit SDL
	SDL_Quit( );
	return 0;
}
