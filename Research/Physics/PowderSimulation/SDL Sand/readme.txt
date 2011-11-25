SDL Sand
-----------------
The SDL Sand game (The Falling SDL-Sand Game) is a C++ implementation of the original 'World of
Sand' (and later 'Hell of Sand') game implemented in JAVA. SDL Sand uses the SDL (Simple DirectMedia
Library - http://www.libsdl.org/) library for screen output. Therefore the implementation can
possibly run on every platform supported by SDL.

The aim is to create a faster implementation of the game and possibly extend its features.


Links
-----------------
Project link: http://sourceforge.net/projects/sdlsand
The originial World of Sand from DOFI-BLOG (http://ishi.blog2.fc2.com/blog-entry-158.html)
The later version - Hell of Sand from DOFI-BLOG (http://ishi.blog2.fc2.com/blog-entry-164.html)
A Falling Sand Game forum (http://www.fallingsandgame.com/)
Falling Sand Game hosted (http://chir.ag/stuff/sand/)


Keymap
-----------------
ESC: Exit game
0: Eraser
1: Draw walls
2: Draw sand
3: Draw water
4: Draw oil
ARROW UP: Double pen size
ARROW DOWN: Half pen size
DEL: Cear screen
Q, A, Z: (Sand)
  Q - increase emitter density
  A - decrease emitter density
  Z - disable / enable emitter
W, S, X: (Water)
  W - increase emitter density
  S - decrease emitter density
  X - disable / enable emitter
E, D, C: (Oil)
  E - increase emitter density
  D - decrease emitter density
  C - disable / enable emitter
R - Draw 20 horinzontal and 20 vertical random lines
T - Erase 20 horinzontal and 20 vertica lines
L - Enable / disable swapping of particles (making water ligther than sand for instance)

Mouse
----------------
Select a brush type using the numbers 0 - 4 and draw while pressing
the left mouse button. If using the right mouse button no continous
line will be drawn between drawing points.

The authors
----------------
Thomas René Sidor (Studying computer science at the university of Copenhagen, Denmark) (Personal homepage: http://www.mcbyte.dk)
Kristian Jensen (Studying computer science at Roskilde University, Denmark)

Developers
----------------


Acknowlegdments
----------------
CCmdLine - command line parser by Chris Losinger (http://www.codeproject.com/cpp/ccmdline.asp)
SDL - Simple DirectMedia Library (http://www.libsdl.org)