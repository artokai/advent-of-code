namespace Artokai.AOC.Puzzles.Y2016.D20;

public record Range(uint Start, uint End)
{
    public bool Contains(uint value) => value >= Start && value <= End;

    public bool Overlaps(Range other) => Start <= other.End && End >= other.Start;

    public Range Merge(Range other) => new Range(Math.Min(Start, other.Start), Math.Max(End, other.End));

    public uint Size() => End - Start + 1;
}