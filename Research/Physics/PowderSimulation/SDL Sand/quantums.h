#pragma once

//Screen size
const int FIELD_WIDTH = 1024;
const int FIELD_HEIGHT = 768;

bool implementParticleSwaps = true;

enum ParticleType
{
	NOTHING = 0,
	WALL,
	SAND,
	WATER,
	OIL,
};

//Instead of using a two dimensional array
// we'll use a simple array to improve speed
// vs = virtual screen
ParticleType vs[FIELD_WIDTH*FIELD_HEIGHT];
int vsCountdown[FIELD_WIDTH*FIELD_HEIGHT];

enum UpdateMethod
{
	UM_VERTICAL_TOP_TO_BOTTOM = 0,
	UM_VERTICAL_TO_BOTTOM_TOP,
	UM_MOVEMENT_RANDOM,
	UM_MOVEMENT_REGULAR,
	UM_TOTAL
};

UpdateMethod updateVertical = UM_VERTICAL_TOP_TO_BOTTOM;
UpdateMethod updateMovement = UM_MOVEMENT_RANDOM;

inline void ClearQuantumBuffer(void * buffer, int initValue, size_t sizeofElement)
{
	memset(buffer, initValue, FIELD_WIDTH*FIELD_HEIGHT*sizeofElement);
}

// Performing the movement logic of a given particle. The argument 'type'
// is passed so that we don't need a table lookup when determining the
// type to set the given particle to - i.e. if the particle is SAND then the
// passed type will be MOVEDSAND
inline void MoveQuantum(int x, int y)
{
	// We'll only calculate these indicies once for optimization purpose
	int same = x+(FIELD_WIDTH*y);
	ParticleType type = vs[same];

	if (type == NOTHING || type == WALL || vsCountdown[same] < 1)
		return;

	//Creating a random int
	int r = rand();

	if ( updateMovement == UM_MOVEMENT_RANDOM && r < RAND_MAX / 13 ) // This makes it fall unevenly
		return;

	// We'll only calculate these indicies once for optimization purpose
	int below = x+((y+1)*FIELD_WIDTH);

	//If nothing below then just fall
	if ( 
		vs[below] == NOTHING 
		&& (updateMovement != UM_MOVEMENT_RANDOM || r % 8) //rand() % 8 makes it spread
	)
	{
		vs[below] = type;
		vsCountdown[below] -= 1;
		vs[same] = NOTHING;
		return;
	}

	// Peform 'realism' logic?
	if(implementParticleSwaps)
	{
		//Making water lighter than sand
		if(type == SAND && (vs[below] == WATER))
		{
			vs[below] = SAND;
			vsCountdown[below] -= 1;
			vs[same] = WATER;
			return;
		}

		//Making sand lighter than oil
		if(
			type == SAND && (vs[below] == OIL) 
			&& rand() % 5 == 0 //Making oil dense so that sand falls slower
		)
		{
			vs[below] = SAND;
			vsCountdown[below] -= 1;
			vs[same] = OIL;
			return;
		}

		//Making oil lighter than water
		if(type == WATER && (vs[below] == OIL))
		{
			vs[below] = WATER;
			vsCountdown[below] -= 1;
			vs[same] = OIL;
			return;
		}
	}

	//Randomly select right or left first
	int flowShift = rand() % 2 == 0 ? -1 : 1;

	//Add to sideways flow
	int sidewayFlowMaxStep = 5;
	if(
		(
			(vs[(x+1)+((y-1)*FIELD_WIDTH)] != NOTHING && vs[(x+1)+(FIELD_WIDTH*y)] != NOTHING) || 
			(vs[(x-1)+((y-1)*FIELD_WIDTH)] != NOTHING && vs[(x-1)+(FIELD_WIDTH*y)] != NOTHING)
		) 
		&& (x-sidewayFlowMaxStep)>0 && (x+sidewayFlowMaxStep) < FIELD_WIDTH
	)
	{
		flowShift *= rand()%(sidewayFlowMaxStep+1);
	}
	else if (vs[x+(y+1)*FIELD_WIDTH] == NOTHING)
	{
		vs[x+(y+1)*FIELD_WIDTH] = type;
		vsCountdown[x+(y+1)*FIELD_WIDTH] -= 1;
		vs[same] = NOTHING;
		return;
	}

	// We'll only calculate these indicies once for optimization purpose
	int firstdown = (x+flowShift)+((y+1)*FIELD_WIDTH);
	int seconddown = (x-flowShift)+((y+1)*FIELD_WIDTH);
	int first = (x+flowShift)+(FIELD_WIDTH*y);
	int second = (x-flowShift)+(FIELD_WIDTH*y);

	// The place below (x,y+1) is filled with something, then check (x+sign,y+1) and (x-sign,y+1) 
	// We chose sign randomly to randomly check eigther left or right
	if ( vs[firstdown] == NOTHING )
	{
		vs[firstdown] = type;
		vsCountdown[firstdown] -= 1;
		vs[same] = NOTHING;
	}
	else if ( vs[seconddown] == NOTHING )
	{
		vs[seconddown] = type;
		vsCountdown[seconddown] -= 1;
		vs[same] = NOTHING;
	}
	//If (x+sign,y+1) is filled then try (x+sign,y) and (x-sign,y)
	else if (vs[first] == NOTHING )
	{
		vs[first] = type;
		vsCountdown[first] -= 1;
		vs[same] = NOTHING;
	}
	else if (vs[second] == NOTHING )
	{
		vs[second] = type;
		vsCountdown[second] -= 1;
		vs[same] = NOTHING;
	}
}

inline void UpdateVirtualScreenLine(int y)
{
	// Due to biasing when iterating through the scanline from left to right,
	// we now chose our direction randomly per scanline.
	if (rand() % 2 != 0)
		for(int x = FIELD_WIDTH-2; x > 0 ; x--) MoveQuantum(x,y);
	else
		for(int x = 1; x < FIELD_WIDTH - 1; x++) MoveQuantum(x,y);
}

// Updating the particle system (virtual screen) pixel by pixel
inline void UpdateVirtualScreen()
{
	ClearQuantumBuffer((void *)vsCountdown, 1, sizeof(int));

	if (updateVertical == UM_VERTICAL_TOP_TO_BOTTOM)
		for(int y = FIELD_HEIGHT-2; y > 0; y--)
			UpdateVirtualScreenLine(y);
	else
		for(int y = 1; y < FIELD_HEIGHT-1; y++)
			UpdateVirtualScreenLine(y);
}
