using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D06;

[PuzzleInfo(year: 2016, day: 6, part: 2, title: "Signals and Noise")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var columns = Input.AsColumns<char>([]);
        return new string(
            columns.Select(col =>
            col.GroupBy(c => c)
                .OrderBy(g => g.Count())
                .First()
                .Key
            ).ToArray()
        );
    }
}
