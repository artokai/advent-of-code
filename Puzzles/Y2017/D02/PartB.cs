using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D02;

[PuzzleInfo(year: 2017, day: 2, part: 2, title: "Corruption Checksum")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var spreadsheet = Input.AsLists<int>();
        var checksum = 0;
        foreach (var row in spreadsheet)
        {
            for (var i = 0; i < row.Count -1; i++)
            {
                for (var j = i + 1; j < row.Count; j++)
                {
                    var a = Math.Max(row[i], row[j]);
                    var b = Math.Min(row[i], row[j]);
                    if (a % b == 0)
                    {
                        checksum += a / b;
                        break;
                    }                    
                }
            }
        }
        return checksum.ToString();
    }
}
