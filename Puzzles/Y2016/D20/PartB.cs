using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D20;

[PuzzleInfo(year: 2016, day: 20, part: 2, title: "Firewall Rules")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        // Sort ranges by start so potentially overlapping ranges are adjacent
        var ranges = InputParser.Parse(Input)
            .OrderBy(r => r.Start)
            .ToList();

        var mergedRanges = new List<Range> { ranges[0] };
        ranges.RemoveAt(0);
        foreach (var range in ranges)
        {
            var last = mergedRanges[^1];
            if (last.Overlaps(range) || last.End + 1 == range.Start)
            {
                mergedRanges[^1] = last.Merge(range);
            }
            else
            {
                mergedRanges.Add(range);
            }
        }

        var blockedCount = mergedRanges.Sum(r => r.Size());
        return (4294967295L + 1 - blockedCount).ToString();
    }
}
