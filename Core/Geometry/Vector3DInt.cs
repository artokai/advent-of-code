namespace Artokai.AOC.Core.Geometry;

public record struct Vector3DInt(int X, int Y, int Z)
{
    public static Vector3DInt operator +(Vector3DInt a, Vector3DInt b) => new Vector3DInt(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3DInt operator -(Vector3DInt a, Vector3DInt b) => new Vector3DInt(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3DInt operator *(Vector3DInt v, int multiplier) => new Vector3DInt(v.X * multiplier, v.Y * multiplier, v.Z * multiplier);

    public int ManhattanDistance() => System.Math.Abs(X) + System.Math.Abs(Y) + System.Math.Abs(Z);

    public override string ToString() => $"({X}, {Y}, {Z})";
}
