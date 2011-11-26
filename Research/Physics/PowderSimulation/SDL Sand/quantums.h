#pragma once

//Screen size
const int FIELD_WIDTH = 1024;
const int FIELD_HEIGHT = 768;

bool implementParticleSwaps = true;

enum ParticleType
{
	PT_Nothing = 0,
	PT_Wall,
	PT_Sand,
	PT_Water,
	PT_Oil,
	PT_TOTAL
};

const int ParticleUpdateCounters[PT_TOTAL] = {1, 1, 1, 1, 1};

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
// type to set the given particle to - i.e. if the particle is PT_Sand then the
// passed type will be MOVEDSAND
inline void MoveQuantum(int x, int y)
{
	// We'll only calculate these indicies once for optimization purpose
	int same = x+(FIELD_WIDTH*y);
	ParticleType type = vs[same];

	if (type == PT_Nothing || type == PT_Wall || vsCountdown[same] < 1)
		return;

	//Creating a random int
	int r = rand();

	if ( updateMovement == UM_MOVEMENT_RANDOM && r < RAND_MAX / 13 ) // This makes it fall unevenly
		return;

	// We'll only calculate these indicies once for optimization purpose
	int below = x+((y+1)*FIELD_WIDTH);

	//If nothing below then just fall
	if ( 
		vs[below] == PT_Nothing 
		&& (updateMovement != UM_MOVEMENT_RANDOM || r % 8) //rand() % 8 makes it spread
	)
	{
		vs[below] = type;
		vsCountdown[below] -= 1;
		vs[same] = PT_Nothing;
		return;
	}

	// Peform 'realism' logic?
	if(implementParticleSwaps)
	{
		//Making water lighter than sand
		if(type == PT_Sand && (vs[below] == PT_Water))
		{
			vs[below] = PT_Sand;
			vsCountdown[below] -= 1;
			vs[same] = PT_Water;
			return;
		}

		//Making sand lighter than oil
		if(
			type == PT_Sand && (vs[below] == PT_Oil) 
			&& rand() % 5 == 0 //Making oil dense so that sand falls slower
		)
		{
			vs[below] = PT_Sand;
			vsCountdown[below] -= 1;
			vs[same] = PT_Oil;
			return;
		}

		//Making oil lighter than water
		if(type == PT_Water && (vs[below] == PT_Oil))
		{
			vs[below] = PT_Water;
			vsCountdown[below] -= 1;
			vs[same] = PT_Oil;
			return;
		}
	}

	//Randomly select right or left first
	int flowShift = rand() % 2 == 0 ? -1 : 1;

	//Add to sideways flow
	int sidewayFlowMaxStep = 5;
	if(
		(
			(vs[(x+1)+((y-1)*FIELD_WIDTH)] != PT_Nothing && vs[(x+1)+(FIELD_WIDTH*y)] != PT_Nothing) || 
			(vs[(x-1)+((y-1)*FIELD_WIDTH)] != PT_Nothing && vs[(x-1)+(FIELD_WIDTH*y)] != PT_Nothing)
		) 
		&& (x-sidewayFlowMaxStep)>0 && (x+sidewayFlowMaxStep) < FIELD_WIDTH
	)
	{
		flowShift *= rand()%(sidewayFlowMaxStep+1);
	}
	else if (vs[x+(y+1)*FIELD_WIDTH] == PT_Nothing)
	{
		vs[x+(y+1)*FIELD_WIDTH] = type;
		vsCountdown[x+(y+1)*FIELD_WIDTH] -= 1;
		vs[same] = PT_Nothing;
		return;
	}

	// We'll only calculate these indicies once for optimization purpose
	int firstdown = (x+flowShift)+((y+1)*FIELD_WIDTH);
	int seconddown = (x-flowShift)+((y+1)*FIELD_WIDTH);
	int first = (x+flowShift)+(FIELD_WIDTH*y);
	int second = (x-flowShift)+(FIELD_WIDTH*y);

	// The place below (x,y+1) is filled with something, then check (x+sign,y+1) and (x-sign,y+1) 
	// We chose sign randomly to randomly check eigther left or right
	if ( vs[firstdown] == PT_Nothing )
	{
		vs[firstdown] = type;
		vsCountdown[firstdown] -= 1;
		vs[same] = PT_Nothing;
	}
	else if ( vs[seconddown] == PT_Nothing )
	{
		vs[seconddown] = type;
		vsCountdown[seconddown] -= 1;
		vs[same] = PT_Nothing;
	}
	//If (x+sign,y+1) is filled then try (x+sign,y) and (x-sign,y)
	else if (vs[first] == PT_Nothing )
	{
		vs[first] = type;
		vsCountdown[first] -= 1;
		vs[same] = PT_Nothing;
	}
	else if (vs[second] == PT_Nothing )
	{
		vs[second] = type;
		vsCountdown[second] -= 1;
		vs[same] = PT_Nothing;
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
