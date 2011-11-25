#pragma once

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
