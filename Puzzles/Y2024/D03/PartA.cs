using Artokai.AOC.Core;
using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2024.D03;

[PuzzleInfo(year: 2024, day: 3, part: 1, title: "Mull It Over")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var result = 0;
        var r = new Regex(@"mul\((\d{1,3})\s*,\s*(\d{1,3})\)");
        var matches = r.Matches(input);
        foreach (Match match in matches)
        {
            var a = int.Parse(match.Groups[1].Value);
            var b = int.Parse(match.Groups[2].Value);
            result += a * b;
        }

        return result.ToString(); ;
    }
}
