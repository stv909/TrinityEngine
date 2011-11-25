#pragma once

//Screen size
const int FIELD_WIDTH = 1024;
const int FIELD_HEIGHT = 768;

bool implementParticleSwaps = true;

enum ParticleType
{
	NOTHING = 0,
	WALL = 1,
	SAND = 2,
	MOVEDSAND = 3, // TODO: find the purpose of this special type. I guess it's useless.
	WATER = 4,
	MOVEDWATER = 5, // TODO: find the purpose of this special type. I guess it's useless.
	OIL = 6,
	MOVEDOIL = 7 // TODO: find the purpose of this special type. I guess it's useless.
};

//Instead of using a two dimensional array
// we'll use a simple array to improve speed
// vs = virtual screen
ParticleType vs[FIELD_WIDTH*FIELD_HEIGHT];

// Performing the movement logic of a given particle. The argument 'type'
// is passed so that we don't need a table lookup when determining the
// type to set the given particle to - i.e. if the particle is SAND then the
// passed type will be MOVEDSAND
inline void MoveQuantum(int x, int y, ParticleType type)
{
	//Creating a random int
	int r = rand();

	if ( r < RAND_MAX / 13 ) return; // This makes it fall unevenly

	// We'll only calculate these indicies once for optimization purpose
	int below = x+((y+1)*FIELD_WIDTH);
	int same = x+(FIELD_WIDTH*y);

	//If nothing below then just fall
	if ( vs[below] == NOTHING && r % 8) //rand() % 8 makes it spread
	{
		vs[below] = type;
		vs[same] = NOTHING;
		return;
	}

	// Peform 'realism' logic?
	if(implementParticleSwaps)
	{
		//Making water lighter than sand
		if(type == MOVEDSAND && (vs[below] == WATER))
		{
			vs[below] = MOVEDSAND;
			vs[same] = WATER;
			return;
		}

		//Making sand lighter than oil
		if(type == MOVEDSAND && (vs[below] == OIL) && (rand() % 5 == 0)) //Making oil dense so that sand falls slower
		{
			vs[below] = MOVEDSAND;
			vs[same] = OIL;
			return;
		}

		//Making oil lighter than water
		if(type == MOVEDWATER && (vs[below] == OIL))
		{
			vs[below] = MOVEDWATER;
			vs[same] = OIL;
			return;
		}
	}

	//Randomly select right or left first
	int sign = rand() % 2 == 0 ? -1 : 1;

	//Add to sideways flow
	if(((vs[(x+1)+((y-1)*FIELD_WIDTH)] !=  NOTHING && vs[(x+1)+(FIELD_WIDTH*y)] != NOTHING) || (vs[(x-1)+((y-1)*FIELD_WIDTH)] != NOTHING && vs[(x-1)+(FIELD_WIDTH*y)] != NOTHING)) && (x-5)>0 && (x+5) < FIELD_WIDTH)
		sign *= rand()%5;

	// We'll only calculate these indicies once for optimization purpose
	int firstdown = (x+sign)+((y+1)*FIELD_WIDTH);
	int seconddown = (x-sign)+((y+1)*FIELD_WIDTH);
	int first = (x+sign)+(FIELD_WIDTH*y);
	int second = (x-sign)+(FIELD_WIDTH*y);

	// The place below (x,y+1) is filled with something, then check (x+sign,y+1) and (x-sign,y+1) 
	// We chose sign randomly to randomly check eigther left or right
	if ( vs[firstdown] == NOTHING )
	{
		vs[firstdown] = type;
		vs[same] = NOTHING;
	}
	else if ( vs[seconddown] == NOTHING )
	{
		vs[seconddown] = type;
		vs[same] = NOTHING;
	}
	//If (x+sign,y+1) is filled then try (x+sign,y) and (x-sign,y)
	else if (vs[first] == NOTHING )
	{
		vs[first] = type;
		vs[same] = NOTHING;
	}
	else if (vs[second] == NOTHING )
	{
		vs[second] = type;
		vs[same] = NOTHING;
	}
}

// Updating a virtual pixel
inline void UpdateVirtualPixel(int x, int y) // TODO: refactor this strange function with x -> MOVEDx trik
{
	switch (vs[x+(FIELD_WIDTH*y)])
	{
	case SAND:
		MoveQuantum(x,y,MOVEDSAND);
		break;
	case WATER:
		MoveQuantum(x,y,MOVEDWATER);
		break;
	case OIL:
		MoveQuantum(x,y,MOVEDOIL);
		break;				
	}
}

// Updating the particle system (virtual screen) pixel by pixel
inline void UpdateVirtualScreen()
{
	for(int y = FIELD_HEIGHT-2; y > 0; y--)
	{
		// Due to biasing when iterating through the scanline from left to right,
		// we now chose our direction randomly per scanline.
		if (rand() & 2)
			for(int x = FIELD_WIDTH-2; x > 0 ; x--) UpdateVirtualPixel(x,y);
		else
			for(int x = 1; x < FIELD_WIDTH - 1; x++) UpdateVirtualPixel(x,y);
	}
}

//---

// Creates particle in given place of field
void SetQuantum(int xpos, int ypos, ParticleType type)
{
	vs[xpos+(FIELD_WIDTH*ypos)] = type;
}

void ConvertMoveQuantumsToRegularQuantums()
{
	//Iterating through each pixe height first
	for(int y=0;y<FIELD_HEIGHT;y++)
	{
		//Width
		for(int x=0;x<FIELD_WIDTH;x++)
		{
			int same = x+(FIELD_WIDTH*y);

			//If the type is of MOVEDx then set it to x
			switch(vs[same])
			{
			case MOVEDSAND:
				vs[same] = SAND;
				break;
			case MOVEDWATER:
				vs[same] = WATER;
				break;
			case MOVEDOIL:
				vs[same] = OIL;
				break;
			}
		}
	}
}

//To emit or not to emit
bool emitSand = true;
bool emitWater = true;
bool emitOil = true;

//Initial density of emitters
float oilDens = 0.3f;
float sandDens = 0.3f;
float waterDens = 0.3f;

// Emitting a given particletype at (x,o) width pixels wide and
// with a p density (probability that a given pixel will be drawn 
// at a given position withing the width)
void Emit(int x, int width, ParticleType type, float p)
{
	for (int i = x - width/2; i < x + width/2; i++)
	{
		if ( rand() < (int)(RAND_MAX * p) ) vs[i+FIELD_WIDTH] = type;
	}
}

void UpdateQuantumEmmiters()
{
	//To emit or not to emit
	if(emitSand)
		Emit(FIELD_WIDTH/4, 20, SAND, sandDens);
	if(emitWater)
		Emit(FIELD_WIDTH/4*2, 20, WATER, waterDens);
	if(emitOil)
		Emit(FIELD_WIDTH/4*3, 20, OIL, oilDens);
}

void ClearQuantumsFromBottomLine()
{
	//Clear bottom line
	for (int i=0; i< FIELD_WIDTH; i++) SetQuantum(i, FIELD_HEIGHT-1, NOTHING);
}

//Cearing the partice system
void ClearQuantums()
{
	for(int w = 0; w < FIELD_WIDTH ; w++)
	{
		for(int h = 0; h < FIELD_HEIGHT; h++)
		{
			SetQuantum(w, h, NOTHING);
		}
	}
}

void InitQuantums()
{
	ClearQuantums();

	// Set initial seed
	srand( (unsigned)time( NULL ) );
}

void UpdateQuantums()
{
	UpdateQuantumEmmiters();
	ClearQuantumsFromBottomLine();
	// Update the virtual screen (performing particle logic)
	UpdateVirtualScreen();
	// Clean up temporary MOVED-information from the field
	ConvertMoveQuantumsToRegularQuantums(); // TODO: Try to make things without this logic and MOVEDx types. // WARNING: these slows down computation. If this logic will be placed to DrawScene, we avoid extra-iteration of all quantums in the field.
}
