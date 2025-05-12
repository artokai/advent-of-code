using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D10;

[PuzzleInfo(year: 2016, day: 10, part: 2, title: "Balance Bots")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var outputs = Simulation.Run(Input);
        var retval = outputs[0] * outputs[1] * outputs[2];
        return retval.ToString();
    }
}
