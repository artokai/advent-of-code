using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D16;

[PuzzleInfo(year: 2016, day: 16, part: 2, title: "Dragon Checksum")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        // Brute forcing large input size like this is not efficient,
        // We could optimize it creating the checksum incrementally without
        // generating the full dragon curve.
        //
        // But it works in reasonable time and does not run out of memory, 
        // so let's go with this.
        return Solver.Solve(Input.AsSingleLine(), 35_651_584);
    }
}
