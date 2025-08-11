using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D17;

[PuzzleInfo(year: 2016, day: 17, part: 2, title: "Two Steps Forward")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var passcode = Input.AsSingleLine().Trim();
        var longest = Solver.Solve(passcode).LastOrDefault();
        return longest == null ? "No solutions found!" : longest.Length.ToString();
    }
}
