namespace _3D_Engine.Engine;

internal readonly struct Color4
{
    public static Color4 White => new(255, 255, 255, 255);
    public static Color4 Red => new(255, 0, 0, 255);
    public static Color4 Green => new(0, 255, 0, 255);
    public static Color4 Blue => new(0, 0, 255, 255);
    public static Color4 Orange => new(240, 100, 15, 255);
    public static Color4 Yellow => new(225, 250, 20, 255);
    public static Color4 Pink => new(250, 70, 240, 255);
    public static Color4 Purple => new(100, 0, 140, 255);
    public static Color4 Gold => new(155, 155, 45, 255);

    public byte R => r;
    public byte G => g;
    public byte B => b;
    public byte A => a;

    private readonly byte b;
    private readonly byte g;
    private readonly byte r;
    private readonly byte a;

    public Color4(byte r, byte g, byte b, byte a)
    {
        this.a = a;
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
