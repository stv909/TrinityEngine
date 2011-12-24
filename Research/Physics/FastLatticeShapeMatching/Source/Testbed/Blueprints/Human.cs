using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class Human
        {
            public static bool[,] blueprint = new bool[50, 50];

            static Human()
            {
                CreateHuman(-15, -25);
                //Flip();
            }

            static void Flip()
            {
                bool[,] temp = new bool[50, 50];
                int x, y;
                for (x = 0; x < 50; x++)
                {
                    for (y = 0; y < 50; y++)
                    {
                        temp[x, 50 - y - 1] = blueprint[x, y];
                    }
                }
                blueprint = temp;
            }

            static void CreateHuman(int offX, int offY)
            {
                int x, y;
                //*
                // Arms
                for (x = 15; x < 33; x++)
                {
                    for (y = 38; y < 40; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Left hand
                blueprint[offX + 16, offY + 40] = true;

                // Right hand
                blueprint[offX + 31, offY + 40] = true;

                // Torso
                for (x = 21; x < 27; x++)
                {
                    for (y = 34; y < 41; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Torso
                for (x = 22; x < 26; x++)
                {
                    for (y = 31; y < 42; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Head
                for (x = 22; x < 26; x++)
                {
                    for (y = 43; y < 46; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Head
                for (x = 23; x < 25; x++)
                {
                    for (y = 43; y < 47; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Neck
                for (x = 23; x < 25; x++)
                {
                    for (y = 35; y < 47; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Left leg
                for (x = 21; x < 23; x++)
                {
                    for (y = 25; y < 33; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Right leg
                for (x = 25; x < 27; x++)
                {
                    for (y = 25; y < 33; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Left foot
                for (x = 20; x < 23; x++)
                {
                    for (y = 25; y < 27; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }

                // Right foot
                for (x = 25; x < 28; x++)
                {
                    for (y = 25; y < 27; y++)
                    {
                        blueprint[offX + x, offY + y] = true;
                    }
                }
            }
        }
    }
}
