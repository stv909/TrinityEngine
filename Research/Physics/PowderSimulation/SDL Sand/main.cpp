#ifdef WIN32
#pragma comment(lib, "../SDL/lib/SDL.lib")
#endif

#include <tchar.h>
#include <cstdlib>
#include <ctime>

#include "../SDL/includes/SDL.h"
#include "math.h"

#include "quantums.h"
#include "render.h"
#include "input.h"

void Init()
{
	InitInput();
	InitQuantums();
	InitRender();
}

void Update()
{
	UpdateInput();
	UpdateQuantums();
	UpdateRender();
}

void Shutdown()
{
	SDL_Quit(); // shutdown all SDL subsystems
}

int _tmain(int argc, _TCHAR* argv[])
{
	Init();
	while (done == 0)
	{
		Update();
		PrintFPS();
	}
	Shutdown();
	return 0;
}
