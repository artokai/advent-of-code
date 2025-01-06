namespace Artokai.AOC.Puzzles.Y2024.D14;

public struct Robot
{
    public int x;
    public int y;
    public int dx;
    public int dy;

    public override string ToString()
    {
        return $"p={x},{y} v={dx},{dy}";
    }
}