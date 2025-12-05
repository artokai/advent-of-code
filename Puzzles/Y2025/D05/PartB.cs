using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D05;

[PuzzleInfo(year: 2025, day: 5, part: 2, title: "Cafeteria")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var parts = Input.SplitOnEmptyLines();
        var ranges = parts[0].AsLines()
            .Select(IdRange.Parse)
            .OrderBy(r => r.Start)
            .ToList();

        var i = 0;
        while (i < ranges.Count - 1)
        {
            var current = ranges[i];
            var merged = true;
            while (merged && i < ranges.Count - 1)
            {
                var next = ranges[i + 1];
                merged = current.TryMerge(next);
                if (merged)
                {
                    ranges.RemoveAt(i + 1);
                }
            }
            i++;
        }

        return ranges.Sum(r => r.Size).ToString();
    }
}
