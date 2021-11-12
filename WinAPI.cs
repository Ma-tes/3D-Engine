using System;
using System.Runtime.InteropServices;
using System.Drawing;
namespace _3D_Engine
{
    public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    internal class WinAPI
    {
        public static WndProc MyDefWndProc = MyWndProc;
        public static Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
        public static IntPtr WindowHandler = IntPtr.Zero;
        public void Update() 
        {
            MSG msg = new MSG();
            if (WindowHandler == IntPtr.Zero) 
            {
                throw new Exception("Please set WindowHandler to WinAPI");
            }

            SetTimer(WindowHandler, IntPtr.MaxValue, 1, IntPtr.Zero);
            while(true)
            {
                    PeekMessage(ref msg, WindowHandler, 0, 0, 1);
                    OnUpdate();
                    TranslateMessage(ref msg);
                    DispatchMessage(ref msg);
            }
        }
        public virtual void OnUpdate() 
        {
        }
        public enum ExtendedWindowStyles : uint
        {
            WM_PAINT = 2,
            WM_LBUTTONUP = 0x0202
        };
        public enum WindowStyles : uint
        {
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_GROUP = 0x20000,  
            WS_HSCROLL = 0x100000,  
            WS_MAXIMIZE = 0x1000000,  
            WS_MAXIMIZEBOX = 0x10000,  
            WS_MINIMIZE = 0x20000000,  
            WS_MINIMIZEBOX = 0x20000,  
            WS_OVERLAPPED = 0x0,  
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,  
            WS_POPUP = 0x80000000u,  
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,  
            WS_SIZEFRAME = 0x40000,  
            WS_SYSMENU = 0x80000,  
            WS_TABSTOP = 0x10000,  
            WS_VISIBLE = 0x10000000,  
            WS_VSCROLL = 0x200000
        };
        public enum WindowStylesEx : uint  
        {  
            WS_EX_ACCEPTFILES = 0x00000010,  
            WS_EX_APPWINDOW = 0x00040000,  
            WS_EX_CLIENTEDGE = 0x00000200,  
            WS_EX_COMPOSITED = 0x02000000,  
            WS_EX_CONTEXTHELP = 0x00000400,  
            WS_EX_CONTROLPARENT = 0x00010000,  
            WS_EX_DLGMODALFRAME = 0x00000001,  
            WS_EX_LAYERED = 0x00080000,  
            WS_EX_LAYOUTRTL = 0x00400000,  
            WS_EX_LEFT = 0x00000000,  
            WS_EX_LEFTSCROLLBAR = 0x00004000,  
            WS_EX_LTRREADING = 0x00000000,  
            WS_EX_MDICHILD = 0x00000040,  
            WS_EX_NOACTIVATE = 0x08000000,  
            WS_EX_NOINHERITLAYOUT = 0x00100000,  
            WS_EX_NOPARENTNOTIFY = 0x00000004,  
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,  
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,  
            WS_EX_RIGHT = 0x00001000,  
            WS_EX_RIGHTSCROLLBAR = 0x00000000,  
            WS_EX_RTLREADING = 0x00002000,  
            WS_EX_STATICEDGE = 0x00020000,  
            WS_EX_TOOLWINDOW = 0x00000080,  
            WS_EX_TOPMOST = 0x00000008,  
            WS_EX_TRANSPARENT = 0x00000020,  
            WS_EX_WINDOWEDGE = 0x00000100  
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WNDCLASSEX
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int style;
            public IntPtr lpfnWndProc; 
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }
        public struct POINT
        {
            public double x;
            public double y;

        }
        public struct MSG
        {
            IntPtr hWnd;
            public uint message;
            UIntPtr wParam;
            IntPtr lParam;
            int time;
            POINT pt;
            int lPrivate;
        }
        
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
        public static bool IsDown = false;
        private static IntPtr MyWndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParamnt)
        {
            if(uMsg == (uint)ExtendedWindowStyles.WM_LBUTTONUP)
            {
                IsDown = true;
                Console.WriteLine("IsDown");
            }
            else
                IsDown = false;
            if (uMsg == 15) // Thats WM_paint
            {
                graphics = Graphics.FromHwnd(hWnd);
            }
            if (uMsg == 0x0014) 
            {
            }
            if (uMsg == 0x0113) 
            {
                InvalidateRect(WindowHandler, IntPtr.Zero, true);
            }
            return DefWindowProc(hWnd, uMsg, wParam, lParamnt);
        }
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern System.UInt16 RegisterClassEx(ref WNDCLASSEX lpWndClassEx);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

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
                                            int wRemoveMsg //TODO Enum like PM_NOREMOVE and PM_REMOVE
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
    }    
}
























































