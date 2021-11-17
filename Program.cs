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
        private Random random = new Random();
        private Rectangle BoxPositions = new Rectangle(80, 80, 51, 51);
        private Point CursorPosition = new Point();
        private Rectangle WindowPosition = new Rectangle(); 

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
            //Console.WriteLine($"[x] = {CursorPosition.X} [y] = {CursorPosition.Y}");
            GetWindowRect(WindowHandler, ref WindowPosition);
            Console.WriteLine($"{WindowPosition.X} {WindowPosition.Left} {CursorPosition.X} {CursorPosition.Y} {Cursor.CursorOnHandle()}");
            graphics.DrawImage(Image.FromFile(@"C:\Users\uzivatel\Desktop\Snímek obrazovky 2021-11-12 220753.jpg"), new Point(BoxPositions.X, BoxPositions.Y));
            graphics.DrawEllipse(new Pen(Color.White, 20), BoxPositions);
        }
    }
}
