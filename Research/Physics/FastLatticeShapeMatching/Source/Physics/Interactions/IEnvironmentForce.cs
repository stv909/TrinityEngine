using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public interface IUpdatableForce
    {
        void Update();
    }

    public interface IEnvironmentForce
	{
		void ApplyForce(IEnumerable particles);
    }
}
