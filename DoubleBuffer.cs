using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Engine
{
    public class DoubleBuffer
    {
        private readonly Bitmap BackupBitmap = new Bitmap(500, 500);

        public Bitmap GetMainBitmap() 
        {
            return BackupBitmap; 
        }

        public void ChangeGraphics(ref Graphics MainGraphics) 
        {
            MainGraphics = Graphics.FromImage(BackupBitmap);
        }
    }
}
