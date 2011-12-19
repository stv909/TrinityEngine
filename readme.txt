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
								+ vs complex static body
							1.1.2.1.1.1.2 Single chunk of soft body
								- vs simple wall
								- vs complex static body
						1.1.2.1.1.2 Dynamic environment
							1.1.2.1.1.2.1 Single particle
								- vs moving object (rotating wall, kinematic body etc.)
									+ visual continuous collision detection edge vs particle
									+ implementation algorithm for edge vs particle CCD (use formulas and equations)
									- correct contact generation for case of colliding of 2 moving bodies:
										- analize activeBody-passiveBody collisions with simplified bodies iteration test algorithm
											+ make simplification
											+ explore deadlock issue when passive body (v > 0) strikes active body (v = 0)
											+ implement zoom-in/zoom-out
											+ fix CCD Helper debug drawing
											- rewrite collision handling cycle
												+ check whole world in every subframe
												+ correct bodies iterating for collision detection: avoid double-checks per frame with conflicting results
												+ test new system in all cases
												+ remove bodyActiveDebug & bodyPassiveDebug, make proper and quality Frozen & UseWallForce flag instead
												- make general refactoring of all algorithms and data structures to reflect new conception
												- restore all debug drawings
										+ simple and obvious method to find timeCoefficient
									- improve zoom-in/zoom-out and processing in world.paused mode
									- rotating impulse wall
								- vs complex dynamic body
									+ velocity modification using Rule of conservative impulses: particle <---infuence---> edge
									> place all algorithms and principles description to Wiki
									- analyse how does Rule of conservative impulses work, debug it
									- avoid particle's energy growth while frequent collidings
							1.1.2.1.1.2.2 Single chunk of soft body
								- just being (drag it in empty scene)
								- vs moving wall
								- vs complex dynamic body
								- self-collision
					1.1.2.1.2 Elastic-Visco-Plastic model of the body
						1.1.2.1.2.1 Linear and non-linear elasticity
						1.1.2.1.2.2 Plasticity
						1.1.2.1.2.3 Viscosity
							1.1.2.1.2.3.1 Pure viscosity
							1.1.2.1.2.3.2 Viscoelastic
							1.1.2.1.2.3.3 Viscoplastic
				1.1.2.2 Fast LSM implementation
			1.1.3 Integration Powder Toys concept with FLSM
			
		1.2 Multicore cloud computing
		1.3 Editor for creating worlds with entities
			1.3.1 Material editor and testing polygone
			1.3.2 Physical body designer
			1.3.3 System of physical bodies editor

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

Falling Sand paradigm
	http://powdertoy.co.uk/ - The Powder Toys. Most known implementation of static interactions.

	http://fallingsandgame.com/ - Forum with multiple implementations of Falling Sand games - parents of The Powder Toys
		http://fallingsandgame.com/sand/NewSand.html - implementation with Zombies (characters in physical environment)
		http://sourceforge.net/projects/sdlsand/ - simple C++ implementation

Deformable Objects
	http://www.alecrivers.com/fastlsm/
		http://realmatter.com/
		http://vimeo.com/16652015
		http://vimeo.com/16652171
	http://grail.cs.washington.edu/projects/deformation/
	http://graphics.ewha.ac.kr/FAST/

Continuous Collision Detection
	http://www.wildbunny.co.uk/blog/2011/04/20/collision-detection-for-dummies/
	http://www.wildbunny.co.uk/blog/2011/03/25/speculative-contacts-an-continuous-collision-engine-approach-part-1/
	http://www.youtube.com/watch?v=0_v46pNsKkI
	http://www.continuousphysics.com/BulletContinuousCollisionDetection.pdf
	http://www.dtecta.com/papers/unpublished04raycast.pdf
	http://www.kuffner.org/james/software/dynamics/mirtich/mirtichThesis.pdf

2D Game Engines
	http://www.stencyl.com/stencylworks/overview/

Liquids
	http://www.youtube.com/watch?NR=1&feature=endscreen&v=KkEP_HA4X3Y

Character Animation
	http://www.youtube.com/watch?v=xXDZZt7TEAo&feature=related
	http://aigamedev.com/premium/interview/near-optimal-character-locomotion/
	http://aigamedev.com/open/article/optimal-continuous-control/
	http://grail.cs.washington.edu/projects/graph-optimal-control/
	http://www.youtube.com/watch?v=JzAEx6d4ow4&feature=related
	http://graphics.cs.ucr.edu/projects/mocsim/mocsim.html

Math Support
	http://math-blog.com/2007/06/02/3-awesome-free-math-programs/
	http://maxima.sourceforge.net/
