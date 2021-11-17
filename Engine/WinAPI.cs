using System;
using System.Runtime.InteropServices;
using System.Drawing;
using _3D_Engine.WinEnums;
using _3D_Engine.WinStructs;

namespace _3D_Engine
{
    public delegate int WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    abstract class WinAPI
    {
        protected  WndProc MyDefWndProc = MyWndProc;
        protected  Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
        protected static IntPtr WindowHandler = IntPtr.Zero;
        protected  PS PaintStructure = new();
        public void Update()
        {
            MSG msg = new MSG();
            graphics = Graphics.FromHwnd(WindowHandler);
            DoubleBuffer doubleBuffer = new DoubleBuffer();

            if (WindowHandler == IntPtr.Zero)
            {
                throw new Exception("Please set WindowHandler to WinAPI");
            }

            float fps = 0;
            int frame = 0;
            float elapsedTime = DateTime.Now.Second;
            while (true)
            {
                frame++;
                if ((DateTime.Now.Second - elapsedTime) >= 1) 
                {
                    fps = frame;
                    frame = 0;
                   elapsedTime = DateTime.Now.Second;
                }
                doubleBuffer.ChangeGraphics(ref graphics);
                graphics.Clear(Color.Black);

                //Message stuff
                PeekMessage(ref msg, WindowHandler, 0, 0, 1);
                TranslateMessage(ref msg);
                OnUpdate();
                DispatchMessage(ref msg);

                graphics = Graphics.FromHwnd(WindowHandler);
                graphics.DrawImage(doubleBuffer.GetMainBitmap(), new Point(0,0));
                SetWindowText(WindowHandler, $"{fps}");
            }
        }
        public virtual void OnUpdate()
        {
        }

        #region DLL_IMPORTS
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWindowEx
            (
                WindowStylesEx wExStyle,
                string lpClassName,
                //UInt16 ClassResult,
                [MarshalAs(UnmanagedType.LPStr)]
                string lpWindowName,
                WindowStyles dwStyle,
                int x,
                int y,
                int nWidth,
                int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInstance,
                IntPtr lpParam
            );
        private static int MyWndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParamnt)
        {
            return DefWindowProc(hWnd, uMsg, wParam, lParamnt);
        }
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint RegisterClassEx(ref WNDCLASSEX lpWndClassEx);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]

        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lp, IntPtr hr, uint RedrawFlags);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool PeekMessage(
                                            ref MSG lpMsg,
                                            IntPtr hWnd,
                                            int wMsgMin,
                                            int wMsgMax,
                                            int wRemoveMsg //TODO Enum PM_NOREMOVE and PM_REMOVE
                                                            );

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool DispatchMessage(ref MSG lpMsg);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, nint lParam);

        [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint SetPixel(IntPtr hnd, int x, int y, uint crColor);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string name);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int key);
        public static bool OnInput(ConsoleKey key)
        {
            return GetAsyncKeyState((int)key) != 0;
        }

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, ref PS lpPaint);

        [DllImport("user32.dll")]
        public static extern IntPtr EndPaint(IntPtr hwnd, ref PS lpPaint);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point coordinates);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle lpRect);
#endregion
    }
}
























































