using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace _3D_Engine
{
    internal class Cursor : WinAPI
    {
        private static Rectangle WindowPositions = new Rectangle();

        private static Point MousePosition = new Point(); 

        public static Vector2 GetCursorPosition() 
        {
            GetCursorPos(ref MousePosition);
            GetWindowRect(WindowHandler, ref WindowPositions);
            return Vector2.Zero();
        }
        public static bool CursorOnHandle() 
        {
            GetCursorPos(ref MousePosition);
            GetWindowRect(WindowHandler, ref WindowPositions);
            return ((MousePosition.X >= WindowPositions.Left) && (MousePosition.X <= WindowPositions.Right)) &&
                   ((MousePosition.Y >= WindowPositions.Top) && (MousePosition.Y <= WindowPositions.Bottom));
        }
    }
}
