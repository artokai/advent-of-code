using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D17;

[PuzzleInfo(year: 2016, day: 17, part: 1, title: "Two Steps Forward")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var passcode = Input.AsSingleLine().Trim();
        var shortest = Solver.Solve(passcode).FirstOrDefault();
        return shortest == null ? "No solution found!" : shortest;
    }
}
