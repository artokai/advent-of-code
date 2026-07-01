using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D10;

[PuzzleInfo(year: 2018, day: 10, part: 2, title: "The Stars Align")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (_, _, seconds) = Solver.Solve(Input, 15_000);
        return seconds.ToString();
    }
}
