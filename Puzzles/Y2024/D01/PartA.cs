using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D01;

[PuzzleInfo(year: 2024, day: 1, part: 1, title: "Historian Hysteria")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var columns = Input.AsColumns<int>();
        var result = columns[0]
            .OrderBy(x => x)
            .Zip(columns[1].OrderBy(x => x), (a, b) => Math.Abs(a - b))
            .Sum();
        return result.ToString();
    }
}
