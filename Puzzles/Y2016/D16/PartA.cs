using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D16;

[PuzzleInfo(year: 2016, day: 16, part: 1, title: "Dragon Checksum")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Solver.Solve(Input.AsSingleLine(), 272);
    }
}
