namespace Artokai.AOC.Puzzles.Y2017.D11;

public record struct QrsCoord(int q, int r, int s)
{
    public static readonly QrsCoord N = new(q: 0, r: -1, s: 1);
    public static readonly QrsCoord NE = new(q: 1, r: -1, s: 0);
    public static readonly QrsCoord SE = new(q: 1, r: 0, s: -1);
    public static readonly QrsCoord S = new(q: 0, r: 1, s: -1);
    public static readonly QrsCoord SW = new(q: -1, r: 1, s: 0);
    public static readonly QrsCoord NW = new(q: -1, r: 0, s: 1);

    public static QrsCoord Parse(string value) => value.ToLower() switch
    {
        "n" => N,
        "ne" => NE,
        "se" => SE,
        "s" => S,
        "sw" => SW,
        "nw" => NW,
        _ => throw new ArgumentException($"Invalid direction: {value}")
    };

    public static QrsCoord operator +(QrsCoord a, QrsCoord b) => new(a.q + b.q, a.r + b.r, a.s + b.s);

    public static QrsCoord operator -(QrsCoord a, QrsCoord b) => new(a.q - b.q, a.r - b.r, a.s - b.s);

    public static QrsCoord operator *(QrsCoord a, int scalar) => new(a.q * scalar, a.r * scalar, a.s * scalar);

    public int Length => (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
}
