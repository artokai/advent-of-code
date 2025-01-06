using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D25;

[PuzzleInfo(year: 2015, day: 25, part: 1, title: "Let It Snow")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        (var row, var column) = ParseInput();

        var x = 1;
        var y = 1;
        var code = 20151125L;

        while (x != column || y != row)
        {
            x += 1;
            y -= 1;
            if (y == 0)
            {
                y = x;
                x = 1;
            }
            code = code * 252533 % 33554393;
        }

        return code.ToString();
    }

    public (int row, int column) ParseInput()
    {
        var re = new Regex(@"row (?<row>\d+), column (?<column>\d+)");
        var manualText = Input.AsSingleLine();
        var match = re.Match(manualText);
        if (!match.Success)
        {
            throw new Exception("Unexpected input format.");
        }
        return (int.Parse(match.Groups["row"].Value), int.Parse(match.Groups["column"].Value));
    }
}
