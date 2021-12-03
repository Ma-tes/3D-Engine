using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace _3D_Engine.Engine;

#nullable enable
internal sealed unsafe class Renderer : IDisposable
{
    public bool IsLocked => data is not null;
    public int Width => (int)width;
    public int Height => (int)height;

    private readonly Bitmap bitmap;
    private readonly Rectangle frame;
    private readonly nint height;
    private readonly nint width;

    private BitmapData? data;
    private nint ptr;
    private nint stride;

    public Renderer(int width, int height)
    {
        bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        frame = new Rectangle(0, 0, width, height);
        this.height = height;
        this.width = width;
    }

    public void Lock()
    {
        if (data is not null)
            return;

        data = bitmap.LockBits(frame, ImageLockMode.ReadWrite, bitmap.PixelFormat);
        ptr = data.Scan0;
        stride = Math.Abs(data.Stride);
    }

    public void Unlock()
    {
        if (data is null)
            return;

        bitmap.UnlockBits(data);
        data = null;
    }

    public void Draw(Graphics graphics)
    {
        if (data is not null)
            throw new InvalidOperationException($"{nameof(Renderer)} must be unlocked before drawing.");
        graphics.DrawImageUnscaledAndClipped(bitmap, frame);
    }

    public void Draw(Graphics graphics, Rectangle target)
    {
        if (data is not null)
            throw new InvalidOperationException($"{nameof(Renderer)} must be unlocked before drawing.");
        graphics.DrawImage(bitmap, target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref Color4 GetRef(int x, int y)
    {
        return ref Unsafe.AsRef<Color4>((void*)(ptr + (x << 2) + y * stride));
    }

    public void Dispose()
    {
        Unlock();
        bitmap.Dispose();
    }
}
