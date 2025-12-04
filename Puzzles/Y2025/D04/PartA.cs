using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D04;

[PuzzleInfo(year: 2025, day: 4, part: 1, title: "Printing Department")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var map = Input.AsCharTable();
        var scanner = new RollScanner(map);
        var result = scanner.Scan().Count();
        return result.ToString();
    }
}
