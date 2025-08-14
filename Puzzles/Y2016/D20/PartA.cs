using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D20;

[PuzzleInfo(year: 2016, day: 20, part: 1, title: "Firewall Rules")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var ranges = InputParser.Parse(Input);
        var candidates = new List<uint>() { 0 };
        candidates.AddRange(ranges.Select(r => r.End + 1));

        var minimum = uint.MaxValue;
        foreach (var candidate in candidates)
        {
            if (ranges.All(r => !r.Contains(candidate)))
            {
                minimum = Math.Min(minimum, candidate);
            }
        }

        return minimum.ToString();
    }
}
