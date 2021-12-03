using System;
using System.Drawing;
using System.Threading;

namespace _3D_Engine.Engine;

#nullable enable
internal sealed class DoubleBufferedRenderer : IDisposable
{
    private Renderer active;
    private Renderer backup;
    private ThreadWorker worker;
    private SemaphoreSlim semaphore = new(1);

    public DoubleBufferedRenderer(int width, int height)
    {
        worker = new ThreadWorker("DoubleBufferedRendererWorker");
        worker.QueueWork(() => {
            active = new Renderer(width, height);
            backup = new Renderer(width, height);

            active.Lock();
        });
        worker.WaitForCompletion();
    }

    public Color4 this[int x, int y]
    {
        get
        {
            return active.GetRef(x, y);
        }

        set
        {
            active.GetRef(x, y) = value;
        }
    }

    public void TryDraw(int x, int y, Color4 color)
    {
        if ((uint)x > active.Width || (uint)y > active.Height)
            return;
        this[x, y] = color;
    }

    public void Render(Graphics graphics)
    {
        semaphore.Wait();

        (active, backup) = (backup, active);
        active.Lock();

        worker.QueueWork(() =>
        {
            backup.Unlock();
            backup.Draw(graphics);
            semaphore.Release();
        });
    }

    public void Dispose()
    {
        active.Unlock();
        active.Dispose();
        backup.Dispose();
    }
}
