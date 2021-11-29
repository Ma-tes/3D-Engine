using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Engine
{
    internal class Vector2 
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Vector2(int x, int y) => (X, Y) = (x, y);

        public static Vector2 Zero() 
        {
            return new Vector2(0, 0);
        }
    }
}
