using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Map
{
    public class MapSpot
    {
        public int X;
        public int Y;
        public MapSpot(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static MapSpot operator + (MapSpot m1, MapSpot m2)
        {
            return new MapSpot(m1.X + m2.X, m1.Y + m2.Y);
        }
    }
}
