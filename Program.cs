using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading;
using _3D_Engine.WinEnums;
using _3D_Engine.WinStructs;

namespace _3D_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            WinTestProgram test = new WinTestProgram();
            test.Load();
        }
    }
    internal class WinTestProgram : WinAPI
    {
        public Pen pen = new Pen(Color.White);
        private Random random = new Random();
        private Rectangle BoxPositions = new Rectangle(80, 80, 51, 51);
        private Point CursorPosition = new Point();
        private Rectangle WindowPosition = new Rectangle();
        private Point ObjectPoint = new Point(25, 25);
        private Point LastObjectPoint;
        private Vector2 LastMouseCoord = Vector2.Zero();
        public void Load()
        {
            var wHandle = new WNDCLASSEX
            {
                cbSize = Marshal.SizeOf(typeof(WNDCLASSEX)),
                lpszClassName = "Tests",
                hInstance = Process.GetCurrentProcess().Handle,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(MyDefWndProc),
                hbrBackground = (IntPtr)1 + 1,
                hIcon = IntPtr.Zero,
            };

            Console.WriteLine($"wHandle = {wHandle}");
            ushort handler = (ushort)RegisterClassEx(ref wHandle);
            if(handler == 0)
                Console.WriteLine("Registration error");
            else
                Console.WriteLine($"Handler Ptr = {handler}");

            var windowHandler = WinAPI.CreateWindowEx(WindowStylesEx.WS_EX_TRANSPARENT, "Tests", "Testing Subject",WindowStyles.WS_VISIBLE, 0,0,500,500,IntPtr.Zero,IntPtr.Zero,Process.GetCurrentProcess().Handle,IntPtr.Zero);

            if(windowHandler == IntPtr.Zero)
                Console.WriteLine("Creating error");
            else
                Console.WriteLine($"windowHanlder Ptr = {windowHandler}");
            ShowWindow(windowHandler, 1);
            WindowHandler = windowHandler;
            SetWindowText(WindowHandler, "Sebastiane");
            Update();
        }
        public override void OnUpdate() 
        {
            GetCursorPos(ref CursorPosition);
            Vector2 mouseCoord = Cursor.GetCursorPosition();
            GetWindowRect(WindowHandler, ref WindowPosition);
            BufferSwap.OnGraphics.DrawEllipse(new Pen(Color.White, 2), BoxPositions);
            BoxPositions.X = ObjectPoint.X;
            BoxPositions.Y = ObjectPoint.Y;
            if (OnInput(ConsoleKey.Spacebar)) 
            {
                ObjectPoint = new Point(mouseCoord.X - (BoxPositions.Width / 2), mouseCoord.Y - (BoxPositions.Height / 2));
            }

            if ((mouseCoord.X >= BoxPositions.X && mouseCoord.X <= BoxPositions.X + BoxPositions.Width) && (mouseCoord.Y >= BoxPositions.Y && mouseCoord.Y <= BoxPositions.Y + BoxPositions.Width)) 
            {
                graphics.DrawString("Collide", new Font("Press Start K", 5), Brushes.White, new PointF(5, 5));
                int plusX = mouseCoord.X - LastMouseCoord.X;
                int plusY = mouseCoord.Y - LastMouseCoord.Y;

                for (int i = 0; i < 100; i++)
                {
                    if(plusX != 0)
                        ObjectPoint.X += plusX / Math.Abs(plusX);
                    if(plusY != 0)
                        ObjectPoint.Y += plusY / Math.Abs(plusY);

                    BoxPositions.X = ObjectPoint.X;
                    BoxPositions.Y = ObjectPoint.Y;
                    BufferSwap.OnGraphics.DrawEllipse(new Pen(Color.White, 2), BoxPositions);
                }
            }
            else
                LastMouseCoord = mouseCoord;

            BufferSwap.OnGraphics.DrawString($"[x] = {mouseCoord.X} [y] = {mouseCoord.Y}", new Font("Press Start K", 5), Brushes.White, new PointF(25, 25));
        }
    }
}
