using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D02;

[PuzzleInfo(year: 2017, day: 2, part: 1, title: "Corruption Checksum")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var spreadsheet = Input.AsLists<int>();
        var checksum = 0;
        foreach (var row in spreadsheet)
        {
            var min = row.Min();
            var max = row.Max();
            checksum += max - min;
        }
        return checksum.ToString();
    }
}
