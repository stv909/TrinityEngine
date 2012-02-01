using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace PhysicsTestbed
{
	public interface IService
	{
        void PreUpdate();
        void PostUpdate();
    }
}
