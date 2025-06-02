using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D15;

[PuzzleInfo(year: 2016, day: 15, part: 1, title: "Timing is Everything")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var discs = Solver.ParseInput(Input);
        return Solver.Solve(discs).ToString();
    }
}
