using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public interface IUpdatable
    {
        void Update();
        void PostUpdate();
    }

    public interface IEnvironmentForce
	{
		void ApplyForce(IEnumerable particles);
    }
}
