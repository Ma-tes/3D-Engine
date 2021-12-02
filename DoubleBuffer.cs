using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Engine
{
    public class BufferSwap
    {

        private Bitmap EmptyBuffer;

        public Bitmap[] Buffers;

        public int Indexer = 0; // 0 is Primary

        public BufferSwap(int width, int heigth) 
        {
            Buffers = new Bitmap[]{ new Bitmap(width, heigth), new Bitmap(width, heigth) };
            EmptyBuffer = new Bitmap(width, heigth);
            Buffers[0].Tag = "Primary";
            Buffers[1].Tag = "Secondary";
        }
        public void RenderNewFrame(ref Graphics hwndGraphics) 
        {
            hwndGraphics.DrawImage(Buffers[ReverseIndexer()], new Point(0,0)); // Primary buffer is write on main buffer 
        }
        public void SwapBuffer() 
        {
            Buffers[ReverseIndexer()] = Buffers[Indexer]; //Primary now is equal to old Secondary
            Buffers[Indexer] = EmptyBuffer;
            Indexer = ReverseIndexer();
        }
        private protected int ReverseIndexer() => (Math.Abs(Indexer - 1));
    }
}
