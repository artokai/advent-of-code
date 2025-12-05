using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D05;

[PuzzleInfo(year: 2025, day: 5, part: 1, title: "Cafeteria")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var parts = Input.SplitOnEmptyLines();
        var ranges = parts[0].AsLines().Select(IdRange.Parse).ToList();
        var ids = parts[1].AsLines().Select(long.Parse).ToList();

        var count = 0;
        foreach (var id in ids)
        {
            if (ranges.Any(r => r.Includes(id)))
            {
                count++;
            }
        }

        return count.ToString();
    }
}
