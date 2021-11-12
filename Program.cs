using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

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
            var wHandle = new WinAPI.WNDCLASSEX
            {
                cbSize = Marshal.SizeOf(typeof(WinAPI.WNDCLASSEX)),
                lpszClassName = "Tests",
                hInstance = Process.GetCurrentProcess().Handle,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(WinAPI.MyDefWndProc),
                hbrBackground = (IntPtr)1 + 1,
                hIcon = IntPtr.Zero,
            };

            Console.WriteLine($"wHandle = {wHandle}");
            ushort handler = WinAPI.RegisterClassEx(ref wHandle);
            if(handler == 0)
                Console.WriteLine("Registration error");
            else
                Console.WriteLine($"Handler Ptr = {handler}");

            var windowHandler = WinAPI.CreateWindowEx(WinAPI.WindowStylesEx.WS_EX_OVERLAPPEDWINDOW, "Tests", "Testing Subject",WinAPI.WindowStyles.WS_VISIBLE, 0,0,500,500,IntPtr.Zero,IntPtr.Zero,Process.GetCurrentProcess().Handle,IntPtr.Zero);

            if(windowHandler == IntPtr.Zero)
                Console.WriteLine("Creating error");
            else
                Console.WriteLine($"windowHanlder Ptr = {windowHandler}");
            WinAPI.ShowWindow(windowHandler, 1);
            WinAPI.WindowHandler = windowHandler;
            SetWindowText(WindowHandler, "Sebastiane");
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Update();
        }
        public override void OnUpdate() 
        {
            BoxPositions.X++;
            graphics.DrawImage(Image.FromFile(@"C:\Users\uzivatel\Desktop\Snímek obrazovky 2021-11-12 220753.jpg"), new Point(BoxPositions.X, BoxPositions.Y));
        }
    }
}
