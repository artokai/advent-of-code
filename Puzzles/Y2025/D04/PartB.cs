using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D04;

[PuzzleInfo(year: 2025, day: 4, part: 2, title: "Printing Department")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var map = Input.AsCharTable();
        var scanner = new RollScanner(map);

        var total = 0;
        var prevTotal = -1;
        while (prevTotal < total)
        {
            prevTotal = total;
            var rolls = scanner.Scan();
            foreach (var pos in rolls)
            {
                map[pos.X, pos.Y] = '.';
                total++;
            }
        }
        return total.ToString();
    }
}
