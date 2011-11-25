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
