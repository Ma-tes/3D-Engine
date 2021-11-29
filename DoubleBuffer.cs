using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Engine
{
    public class DoubleBuffer //TODO Different Name
    {
        private readonly Bitmap BackupBitmap = new Bitmap(500, 500); //TODO Auto X-Y set 

        public Bitmap GetMainBitmap() 
        {
            return BackupBitmap; 
        }

        public void ChangeGraphics(ref Graphics MainGraphics) 
        {
            MainGraphics = Graphics.FromImage(BackupBitmap);
        }
    }
    public static class BufferSwap
    {

        private static Bitmap[] TempBitmaps = new Bitmap[] {new Bitmap(500,500), new Bitmap(500,500)};

        public static int Index = 0;

        public static Graphics OnGraphics = Graphics.FromImage(TempBitmaps[1]); 

        public static void SwapGraphics(ref Graphics hwndGraphics) 
        {
            var lastBuffer = Index; 
            OnGraphics = Graphics.FromImage(TempBitmaps[Index]);
            SwapGrap();
            hwndGraphics.DrawImage(TempBitmaps[lastBuffer], new Point(0, 0));
        }

        public static void SwapGrap() 
        {
            Index = Math.Abs(Index - 1);
        } 
    }
}
