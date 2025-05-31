using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D14;

[PuzzleInfo(year: 2016, day: 14, part: 2, title: "One-Time Pad")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var salt = Input.AsSingleLine().Trim();
        var solver = new Solver(salt, 1000, 2016);
        return solver.Solve(64).ToString();
    }
}
