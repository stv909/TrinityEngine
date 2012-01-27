using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public interface Updatable
    {
        void Update();
        void PostUpdate();
    }

    public interface EnvironmentForce
	{
		void ApplyForce(IEnumerable particles);
    }
}
