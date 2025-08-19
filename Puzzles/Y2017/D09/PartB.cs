using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D09;

[PuzzleInfo(year: 2017, day: 9, part: 2, title: "Stream Processing")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var line = Input.AsSingleLine();
        var parser = new Parser(line);
        var _ = parser.Parse().ToList();
        return parser.GarbageCharCount.ToString();
    }
}
