using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    [Serializable]
    public struct LineSegment
    {
        public Vector2 start;
        public Vector2 end;

        public LineSegment(Vector2 StartPos, Vector2 EndPos)
        {
            this.start = StartPos;
            this.end = EndPos;
        }
    }
}
