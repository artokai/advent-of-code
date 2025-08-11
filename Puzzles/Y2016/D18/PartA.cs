using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D18;

[PuzzleInfo(year: 2016, day: 18, part: 1, title: "Like a Rogue")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine().Trim();
        var safeCount = Solver.Solve(input, 40);
        return safeCount.ToString();
    }
}
