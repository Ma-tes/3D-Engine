using System;
using System.Diagnostics;
using System.Drawing;
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
        private Rectangle BoxPositions = new Rectangle(25, 25, 50, 50);
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
            if (OnInput(ConsoleKey.LeftArrow))
                BoxPositions.X--;
            if (OnInput(ConsoleKey.RightArrow))
                BoxPositions.X++;
            if (OnInput(ConsoleKey.UpArrow))
                BoxPositions.Y--;
            if (OnInput(ConsoleKey.DownArrow))
                BoxPositions.Y++;
            graphics.DrawImage(Image.FromFile(@"C:\Users\uzivatel\Desktop\Snímek obrazovky 2021-11-12 220753.jpg"), new Point(BoxPositions.X, BoxPositions.Y));
            graphics.DrawEllipse(new Pen(Color.Blue), BoxPositions);
        }
    }
}
