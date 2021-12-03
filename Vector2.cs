namespace _3D_Engine;

internal class Vector2
{
    public static Vector2 Zero => new(0, 0);

    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y) => (X, Y) = (x, y);
}
