Trinity Engine. Conceptual design and research.

	Research Directions.

	1. Physical engine
		1.1 Powder Toys + FLSM principles
			1.1.1 Powder Toys concept research
				1.1.1.1+ Falling Sand principle demo
				1.1.1.2 Termodynamics principle demo
				1.1.1.3 Electromagnetics principle demo
				1.1.1.4 Air pressure & velocity principle demo
				1.1.1.5 Chemical reactions principle demo
				1.1.1.6 Propagation of photons principle demo
				1.1.1.7 Radioactive principle demo
			1.1.2 LSM concept research
				1.1.2.1 LSM improvements
					1.1.2.1.1 Collision detetion - use continues approach with iterative subframes
						1.1.2.1.1.1 Static environment
							1.1.2.1.1.1.1 Single particle
								+ vs simple wall
								- vs complex static body
							1.1.2.1.1.1.2 Single chunk of soft body
								- vs simple wall
								- vs complex static body
						1.1.2.1.1.2 Dynamic environment
							1.1.2.1.1.2.1 Single particle
								+ vs moving wall
								- vs complex dynamic body
							1.1.2.1.1.2.2 Single chunk of soft body
								- vs moving wall
								- vs complex dynamic body
								- self-collision
				1.1.2.2 Fast LSM implementation
			1.1.3 Integration Powder Toys with FLSM
			
		1.2 Multicore cloud computing
		1.3 Editor for creating worlds with entities

	2. Programming language system
		2.1 Declarative language
			2.1.1 JSON Dx4
			2.1.2 yEd graphml diagrams
			2.1.3 Overtext editor
		2.2 Imperative language
			2.2.1 C++/C/ASMx86
		2.3 Meta language
			2.3.1 Python

	3. Common engine systems
		3.1 FileSystem
			3.1.1 FileBrowser
			3.1.2 ResourceManagement
			3.1.3 ResourceLinkSystem
		3.2 EntitySystem
		3.3 FixedString, StringBuilder, StringFormat, I18N, L10N

Links:

http://powdertoy.co.uk/ - The Powder Toys. Most known implementation of static interactions.

http://fallingsandgame.com/ - Forum with multiple implementations of Falling Sand games - parents of The Powder Toys
	http://fallingsandgame.com/sand/NewSand.html - implementation with Zombies (characters in physical environment)
	http://sourceforge.net/projects/sdlsand/ - simple C++ implementation

http://www.alecrivers.com/fastlsm/
	http://realmatter.com/
	http://vimeo.com/16652015
	http://vimeo.com/16652171
