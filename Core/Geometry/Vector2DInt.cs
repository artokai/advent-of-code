namespace Artokai.AOC.Core.Geometry;

public record struct Vector2DInt(int X, int Y) {
    public static readonly Vector2DInt Up    = new(X:  0, Y:  -1);
    public static readonly Vector2DInt Right = new(X:  1, Y:  0);
    public static readonly Vector2DInt Down  = new(X:  0, Y: 1);
    public static readonly Vector2DInt Left  = new(X: -1, Y:  0);

    public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b) => new Vector2DInt(a.X + b.X, a.Y + b.Y);
    public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b) => new Vector2DInt(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}