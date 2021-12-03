using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Engine.WinStructs;

public struct PS
{
    public IntPtr hdc;
    public bool fErase;
    public Rectangle rcPaint;
    public bool fRestore;
    public bool fIncUpdate;
    public byte[] rgbReserved;
}
