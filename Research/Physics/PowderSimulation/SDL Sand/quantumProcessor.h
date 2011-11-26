#pragma once

// Creates particle in given place of field
inline void SetQuantum(int xpos, int ypos, ParticleType type)
{
	vs[xpos+(FIELD_WIDTH*ypos)] = type;
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
		Emit(FIELD_WIDTH/4, 20, PT_Sand, sandDens);
	if(emitWater)
		Emit(FIELD_WIDTH/4*2, 20, PT_Water, waterDens);
	if(emitOil)
		Emit(FIELD_WIDTH/4*3, 20, PT_Oil, oilDens);
}

void ClearQuantumsFromBottomLine()
{
	//Clear bottom line
	for (int i=0; i< FIELD_WIDTH; i++) SetQuantum(i, FIELD_HEIGHT-1, PT_Nothing);
}

//Cearing the partice system
void ClearQuantums()
{
	ClearQuantumBuffer((void *)vs, PT_Nothing, sizeof(ParticleType));
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
}
