using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D14;

[PuzzleInfo(year: 2016, day: 14, part: 1, title: "One-Time Pad")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var salt = Input.AsSingleLine().Trim();
        var solver = new Solver(salt, 1000, 0);
        return solver.Solve(64).ToString();
    }
}
