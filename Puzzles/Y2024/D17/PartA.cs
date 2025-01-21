using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D17;

[PuzzleInfo(year: 2024, day: 17, part: 1, title: "Chronospatial Computer")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var c = new Computer();
        return c.Run(46187030);
    }
}
