namespace Artokai.AOC.Puzzles.Y2025.D05;

public class IdRange
{
    public long Start { get; set; }
    public long End { get; set; }

    public IdRange(long start, long end)
    {
        Start = start;
        End = end;
    }

    public bool Includes(long id) => Start <= id && id <= End;

    public bool Overlaps(IdRange other) => !(End < other.Start || other.End < Start);

    public bool TryMerge(IdRange other)
    {
        if (Overlaps(other))
        {
            Start = Math.Min(Start, other.Start);
            End = Math.Max(End, other.End);
            return true;
        }
        return false;
    }

    public long Size => End - Start + 1;

    public static IdRange Parse(string input)
    {
        var parts = input.Split('-');
        return new IdRange(long.Parse(parts[0]), long.Parse(parts[1]));
    }
}