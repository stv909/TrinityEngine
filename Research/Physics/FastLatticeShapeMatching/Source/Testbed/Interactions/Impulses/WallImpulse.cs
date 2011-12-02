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

        private PreCollisionHistory GetNearestCollision(List<PreCollisionHistory> collisions)
        {
            PreCollisionHistory nearestCollision = null;
            foreach (PreCollisionHistory collision in collisions)
            {
                if (nearestCollision == null || nearestCollision.timeCoefficient > collision.timeCoefficient)
                {
                    nearestCollision = collision;
                }
            }
            return nearestCollision;
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
                List<PreCollisionHistory> collisionBuffer = new List<PreCollisionHistory>(4);
                do
                {
                    if (posNext.X < left)
                    {
                        collisionBuffer.Add(new PreCollisionHistory(new Vector2(-t.v.X, t.v.Y), (left - pos.X) / t.v.X));
                    }
                    if (posNext.X > right)
                    {
                        collisionBuffer.Add(new PreCollisionHistory(new Vector2(-t.v.X, t.v.Y), (right - pos.X) / t.v.X));
                    }
                    if (posNext.Y < bottom)
                    {
                        collisionBuffer.Add(new PreCollisionHistory(new Vector2(t.v.X, -t.v.Y), (bottom - pos.Y) / t.v.Y));
                    }
                    if (posNext.Y > top)
                    {
                        collisionBuffer.Add(new PreCollisionHistory(new Vector2(t.v.X, -t.v.Y), (top - pos.Y) / t.v.Y));
                    }

                    if (collisionBuffer.Count > 0)
                    {
                        PreCollisionHistory collision = GetNearestCollision( collisionBuffer );
                        PreCollisionHistory history = new PreCollisionHistory(t.v, collision.timeCoefficient);
                        t.v = collision.v;
                        t.vHistory.Add(history);
                        pos += history.v * history.timeCoefficient;
                        timeCoefficientPrediction -= history.timeCoefficient;
                        posNext = pos + t.v * timeCoefficientPrediction;
                        collisionFound = true;
                        collisionBuffer.Clear();
                    }
                    else
                    {
                        collisionFound = false;
                    }
                }
                while (collisionFound);
                
                if (t.vHistory.Count > 0)
                {
                    t.vHistory.Add(new PreCollisionHistory(t.v, timeCoefficientPrediction));
                }
            }
		}
	}
}
