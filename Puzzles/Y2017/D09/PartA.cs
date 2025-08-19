using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D09;

[PuzzleInfo(year: 2017, day: 9, part: 1, title: "Stream Processing")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var line = Input.AsSingleLine();
        var parser = new Parser(line);
        var groupLevels = parser.Parse();
        return groupLevels.Sum().ToString();
    }
}
