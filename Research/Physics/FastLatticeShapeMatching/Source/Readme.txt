FastLSM 2D Demo
---------------
This demo accompanies the paper "FastLSM: Fast Lattice Shape Matching for Robust Real-Time Deformation" (Rivers and James, 2007), which can be found at http://www.fastlsm.com.

This demo is intended to illustrate the kinds of behavior attainable using the FastLSM algorithm, and to provide an example implementation of the simple 2D case. NOTE that the system implemented here does not take advantage of calculation reuse across regions via shared sub-summations, and therefore achieves only O(w^3) performance per region as opposed to the O(1) performance attainable using FastLSM. This was done because O(w^3) is still fast enough with the low numbers of particles present in 2D and is easier to understand in code.

Instructions
------------
Left-click to drag
Right-click to lock particle or push (depending on selected tool)
