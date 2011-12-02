using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class WallImpulse : EnvironmentImpulse
	{
        public float border = 45;
        public float left = 0, right, bottom = 0, top;

		public WallImpulse(float width, float height)
		{
			this.right = width;
			this.top = height;
		}

		public override void ApplyImpulse(System.Collections.IEnumerable particles)
		{
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;

			foreach(Particle t in particles)
			{
                // impulse & offset collision method
                t.vHistory.Clear();
                double timeCoefficientPrediction = 1.0;
                Vector2 pos = t.x;
                Vector2 posNext = pos + t.v * timeCoefficientPrediction;

                bool collisionFound = false;
                PreCollisionHistory history = null;
                do
                {
                    if (posNext.X < left)
                    {
                        history = new PreCollisionHistory(t.v, (left - pos.X) / t.v.X);
                        t.vHistory.Add(history);
                        t.v.X = -t.v.X;

                        pos += history.v * history.timeCoefficient;
                        timeCoefficientPrediction -= history.timeCoefficient;
                        posNext = pos + t.v * timeCoefficientPrediction;
                        collisionFound = true;
                    }
                    else if (posNext.X > right)
                    {
                        history = new PreCollisionHistory(t.v, (right - pos.X) / t.v.X);
                        t.vHistory.Add(history);
                        t.v.X = -t.v.X;

                        pos += history.v * history.timeCoefficient;
                        timeCoefficientPrediction -= history.timeCoefficient;
                        posNext = pos + t.v * timeCoefficientPrediction;
                        collisionFound = true;
                    }
                    else if (posNext.Y < bottom)
                    {
                        history = new PreCollisionHistory(t.v, (bottom - pos.Y) / t.v.Y);
                        t.vHistory.Add(history);
                        t.v.Y = -t.v.Y;

                        pos += history.v * history.timeCoefficient;
                        timeCoefficientPrediction -= history.timeCoefficient;
                        posNext = pos + t.v * timeCoefficientPrediction;
                        collisionFound = true;
                    }
                    else if (posNext.Y > top)
                    {
                        history = new PreCollisionHistory(t.v, (top - pos.Y) / t.v.Y);
                        t.vHistory.Add(history);
                        t.v.Y = -t.v.Y;

                        pos += history.v * history.timeCoefficient;
                        timeCoefficientPrediction -= history.timeCoefficient;
                        posNext = pos + t.v * timeCoefficientPrediction;
                        collisionFound = true;
                    }
                    else
                    {
                        collisionFound = false;
                    }
                }
                while (collisionFound);
            }
		}
	}
}
