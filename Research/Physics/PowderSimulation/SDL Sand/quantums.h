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
	MOVEDSAND = 3,
	WATER = 4,
	MOVEDWATER = 5,
	OIL = 6,
	MOVEDOIL = 7
};

//Instead of using a two dimensional array
// we'll use a simple array to improve speed
// vs = virtual screen
ParticleType vs[FIELD_WIDTH*FIELD_HEIGHT];

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

// Performing the movement logic of a given particle. The argument 'type'
// is passed so that we don't need a table lookup when determining the
// type to set the given particle to - i.e. if the particle is SAND then the
// passed type will be MOVEDSAND
inline void MoveParticle(int x, int y, ParticleType type)
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

//Drawing a filled circle at a given position with a given radius and a given partice type
void DrawParticles(int xpos, int ypos, int radius, ParticleType type)
{
	for (int x = ((xpos - radius - 1) < 0) ? 0 : (xpos - radius - 1); x <= xpos + radius && x < FIELD_WIDTH; x++)
		for (int y = ((ypos - radius - 1) < 0) ? 0 : (ypos - radius - 1); y <= ypos + radius && y < FIELD_HEIGHT; y++)
		{
			if ((x-xpos)*(x-xpos) + (y-ypos)*(y-ypos) <= radius*radius) vs[x+(FIELD_WIDTH*y)] = type;
		};
}

// Updating a virtual pixel
inline void UpdateVirtualPixel(int x, int y)
{
	switch (vs[x+(FIELD_WIDTH*y)])
	{
	case SAND:
		MoveParticle(x,y,MOVEDSAND);
		break;
	case WATER:
		MoveParticle(x,y,MOVEDWATER);
		break;
	case OIL:
		MoveParticle(x,y,MOVEDOIL);
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

//Cearing the partice system
void Clear()
{
	for(int w = 0; w < FIELD_WIDTH ; w++)
	{
		for(int h = 0; h < FIELD_HEIGHT; h++)
		{
			vs[w+(FIELD_WIDTH*h)] = NOTHING;
		}
	}
}
