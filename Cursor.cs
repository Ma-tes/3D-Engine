using System.Drawing;
namespace _3D_Engine.Engine;

sealed class Cursor : WinAPI
{
    private static Rectangle WindowPositions = new Rectangle();

    private static Point MousePosition = new Point();

    private static Vector2 LastPosition = Vector2.Zero;
    public static Vector2 GetCursorPosition()
    {
        GetCursorPos(ref MousePosition);
        GetWindowRect(WindowHandler, ref WindowPositions);
        if (CursorOnHandle(WindowPositions, MousePosition) == false)
            return LastPosition;
        else
        {
            Vector2 CurrentPosition = new Vector2((MousePosition.X - WindowPositions.Left) - 6, (MousePosition.Y - WindowPositions.Top) - 30);
            LastPosition = CurrentPosition;
            return CurrentPosition;
        }
    }
    public static bool CursorOnHandle(Rectangle windowRectangle, Point cursorPositions)
    {
        return (cursorPositions.X >= windowRectangle.Left && cursorPositions.X <= windowRectangle.Right - windowRectangle.Left) &&
               (cursorPositions.Y >= windowRectangle.Top && cursorPositions.Y <= windowRectangle.Bottom - windowRectangle.Top);
    }
}
