using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D06;

[PuzzleInfo(year: 2016, day: 6, part: 1, title: "Signals and Noise")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var columns = Input.AsColumns<char>([]);
        return new string(
            columns.Select(col =>
            col.GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .First()
                .Key
            ).ToArray()
        );
    }
}
