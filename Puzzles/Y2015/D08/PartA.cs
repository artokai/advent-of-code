using System.Text;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D08;

[PuzzleInfo(year: 2015, day: 8, part: 1, title: "Matchsticks")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        return lines
            .Select(line => line.Length - Unescape(line).Length)
            .Sum()
            .ToString();
    }

    private static string Unescape(string s)
    {
        if (s[0] != '"' || s[^1] != '"')
        {
            throw new Exception("Invalid input string: " + s);
        }

        var result = new StringBuilder();
        var i = 1;
        while (i < s.Length - 1)
        {
            var c = s[i];
            if (c == '\\' && i + 1 < s.Length)
            {
                var c2 = s[i + 1];
                // Escape: \\
                if (c2 == '\\')
                {
                    result.Append('\\');
                    i += 2;
                    continue;
                }

                // Escape: \"
                if (c2 == '"')
                {
                    result.Append('"');
                    i += 2;
                    continue;
                }

                // Escape: \xNN
                if (c2 == 'x' && i + 3 < s.Length)
                {
                    var hex = s.Substring(i + 2, 2);
                    if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out var value))
                    {
                        result.Append((char)value);
                        i += 4;
                        continue;
                    }
                }
            }

            result.Append(c);
            i++;
        }
        return result.ToString();
    }
}
