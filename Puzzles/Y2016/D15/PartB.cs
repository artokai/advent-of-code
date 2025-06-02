using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D15;

[PuzzleInfo(year: 2016, day: 15, part: 2, title: "Timing is Everything")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var discs = Solver.ParseInput(Input);
        discs.Add(new Disc(discs.Count + 1, 11, 0));
        return Solver.Solve(discs).ToString();
    }
}
