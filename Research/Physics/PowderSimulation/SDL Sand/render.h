#pragma once

int renderedParticlesCount = 0;

//THe screen
SDL_Surface *screen;

static Uint32 WATERCOLOR = 0;
static Uint32 OILCOLOR = 0;
static Uint32 WALLCOLOR = 0;
static Uint32 SANDCOLOR = 0;

// Initializing colors
void initColors()
{
	OILCOLOR = SDL_MapRGB(screen->format, 130, 70, 0);
	WATERCOLOR = SDL_MapRGB(screen->format, 0, 50, 255);
	WALLCOLOR = SDL_MapRGB(screen->format, 100, 100, 100);
	SANDCOLOR = SDL_MapRGB(screen->format, 255, 230, 127);
}

//Setting a pixel to a given color
inline void SetPixel16Bit(Uint16 x, Uint16 y, Uint32 pixel)
{
  *((Uint16 *)screen->pixels + y * screen->pitch/2 + x) = pixel;
}

//Drawing our virtual screen to the real screen
static void DrawScene()
{
	renderedParticlesCount = 0;

	//Locking the screen
	if ( SDL_MUSTLOCK(screen) )
	{
		if ( SDL_LockSurface(screen) < 0 )
		{
			return;
		}
	}

	//Clearing the screen with black
	SDL_FillRect(screen,NULL,0);

	//Iterating through each pixe height first
	for(int y=0;y<FIELD_HEIGHT;y++)
	{
		//Width
		for(int x=0;x<FIELD_WIDTH;x++)
		{
			int same = x+(FIELD_WIDTH*y);

			//If the type is of MOVEDx then set it to x and draw it
			switch(vs[same])
			{
			case MOVEDSAND:
				vs[same] = SAND;
			case SAND:
				SetPixel16Bit(x,y,SANDCOLOR);
				renderedParticlesCount++;
				break;
			case MOVEDWATER:
				vs[same] = WATER;
			case WATER:
				SetPixel16Bit(x,y,WATERCOLOR);
				renderedParticlesCount++;
				break;
			case MOVEDOIL:
				vs[same] = OIL;
			case OIL:
				SetPixel16Bit(x,y,OILCOLOR);
				renderedParticlesCount++;
				break;
			case WALL:
				SetPixel16Bit(x,y,WALLCOLOR);
				break;
			}
		}
	}
	//Unlocking the screen
	if ( SDL_MUSTLOCK(screen) )
	{
		SDL_UnlockSurface(screen);
	}
}

// Initializing the screen a
void init()
{
	// Initializing SDL
	if ( SDL_Init( SDL_INIT_VIDEO ) < 0 )
	{
		fprintf( stderr, "Video initialization failed: %s\n",
			SDL_GetError( ) );
		SDL_Quit( );
	}

	//Creating the screen using 16 bit colors
	screen = SDL_SetVideoMode(FIELD_WIDTH, FIELD_HEIGHT, 16, SDL_HWSURFACE|SDL_DOUBLEBUF|SDL_ASYNCBLIT);
	if ( screen == NULL ) {
		fprintf(stderr, "Unable to set video mode: %s\n", SDL_GetError());

	}
	//Setting caption
	SDL_WM_SetCaption("SDL Sand",NULL);
	//Enabeling key repeats
	SDL_EnableKeyRepeat(SDL_DEFAULT_REPEAT_DELAY, SDL_DEFAULT_REPEAT_INTERVAL);

	initColors();
}
