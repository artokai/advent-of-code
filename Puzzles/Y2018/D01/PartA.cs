using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D01;

[PuzzleInfo(year: 2018, day: 1, part: 1, title: "Chronal Calibration")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var result = Input.AsLines()
            .Select(int.Parse)
            .Aggregate(0, (current, change) => current + change);
         return result.ToString();
    }
}
