#pragma once

int penSize = 5;


// The current brush type
ParticleType CurrentParticleType = WALL;

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

