namespace Artokai.AOC.Puzzles.Y2024.D13;

public struct Machine
{
    public double deltaXforA;
    public double deltaYforA;
    public double deltaXforB;
    public double deltaYforB;
    public double targetX;
    public double targetY;

    public long GetPrice()
    {
        var a = (long)Math.Round((targetY - deltaYforB * targetX / deltaXforB) / (deltaYforA - deltaXforA * deltaYforB / deltaXforB));
        var b = (long)Math.Round((targetX - deltaXforA * a) / deltaXforB);
        if (a * deltaXforA + b * deltaXforB != targetX || a * deltaYforA + b * deltaYforB != targetY)
        {
            return 0;
        }
        return 3 * a + 1 * b;
    }
}