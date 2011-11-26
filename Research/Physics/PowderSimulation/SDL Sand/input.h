#pragma once

int penSize = 5;

// The current brush type
ParticleType CurrentParticleType = WALL;

int tick = 0;
int done=0;

int oldx = 0, oldy = 0;

//Mouse button pressed down?
bool down = false;

//Used for calculating the average FPS from NumFrames
const int NumFrames = 20;
int AvgFrameTimes[NumFrames];
int FrameTime = 0;
int PrevFrameTime = 0;
int Index = 0;

void InitInput()
{
	for( int i = 0; i < NumFrames; i++)
		AvgFrameTimes[i] = 0;
}

//Drawing a filled circle at a given position with a given radius and a given partice type
void DrawParticles(int xpos, int ypos, int radius, ParticleType type)
{
	for (int x = ((xpos - radius - 1) < 0) ? 0 : (xpos - radius - 1); x <= xpos + radius && x < FIELD_WIDTH; x++)
		for (int y = ((ypos - radius - 1) < 0) ? 0 : (ypos - radius - 1); y <= ypos + radius && y < FIELD_HEIGHT; y++)
		{
			if ((x-xpos)*(x-xpos) + (y-ypos)*(y-ypos) <= radius*radius) SetQuantum(x, y, type);
		};
}

// Drawing a line
void DrawLine(int newx, int newy, int oldx, int oldy)
{
	if(newx == oldx && newy == oldy)
	{
		DrawParticles(newx,newy,penSize,CurrentParticleType);
	}
	else
	{
		float step = 1.0f / ((abs(newx-oldx)>abs(newy-oldy)) ? abs(newx-oldx) : abs(newy-oldy));
		for (float a = 0; a < 1; a+=step)
			DrawParticles((int)(a*newx+(1-a)*oldx),(int)(a*newy+(1-a)*oldy),penSize,CurrentParticleType); 
	}
}

// Drawing some random wall lines
void DoRandomLines(ParticleType type)
{
	for(int i = 0; i < 20; i++)
	{
		int x1 = rand() % FIELD_WIDTH;
		int x2 = rand() % FIELD_WIDTH;

		float step = 1.0f / FIELD_HEIGHT;
		for (float a = 0; a < 1; a+=step)
			DrawParticles((int)(a*x1+(1-a)*x2),(int)(a*0+(1-a)*FIELD_HEIGHT),penSize,type); 
	}
	for(int i = 0; i < 20; i++)
	{
		int y1 = rand() % FIELD_HEIGHT;
		int y2 = rand() % FIELD_HEIGHT;

		float step = 1.0f / FIELD_WIDTH;
		for (float a = 0; a < 1; a+=step)
			DrawParticles((int)(a*0+(1-a)*FIELD_WIDTH),(int)(a*y1+(1-a)*y2),penSize,type); 
	}
}

// Process input from SDL
void ProcessEvent(SDL_Event event)
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
			ClearQuantums();
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

		case SDLK_F1: 
			updateVertical = UM_VERTICAL_TOP_TO_BOTTOM;
			printf("UM_VERTICAL_TOP_TO_BOTTOM\n");
			break;
		case SDLK_F2: 
			updateVertical = UM_VERTICAL_TO_BOTTOM_TOP;
			printf("UM_VERTICAL_TO_BOTTOM_TOP\n");
			break;
		case SDLK_F3: 
			updateMovement = UM_MOVEMENT_RANDOM;
			printf("UM_MOVEMENT_RANDOM\n");
			break;
		case SDLK_F4: 
			updateMovement = UM_MOVEMENT_REGULAR;
			printf("UM_MOVEMENT_REGULAR\n");
			break;

		case SDLK_F10: 
			DrawLine(0,0,0,0);
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

void UpdateInput()
{
		tick++;

		SDL_Event event;
		//Polling events
		while ( SDL_PollEvent(&event) )
		{
			ProcessEvent(event);
		}

		//If the button is pressed (and no event has occured since last frame due
		// to the polling procedure, then draw at the position (enabeling 'dynamic emitters')
		if(down)
			DrawLine(oldx,oldy,oldx,oldy);
}

void PrintFPS()
{
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