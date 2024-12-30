using Artokai.AOC.Core;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2024.D03;

[PuzzleInfo(year: 2024, day: 3, part: 2, title: "Mull It Over")]
public class PartB : SolverBase
{
    private readonly string doCmd = "do()";
    private readonly string dontCmd = "don't()";

    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var r = new Regex(@"\Gmul\((\d{1,3})\s*,\s*(\d{1,3})\)");
        var i = 0;
        var enabled = true;
        var result = 0;
        while (i < input.Length)
        {
            if (enabled && IsCmdAtIndex(input, dontCmd, i))
            {
                enabled = false;
                i += dontCmd.Length;
                continue;
            }

            if (!enabled && IsCmdAtIndex(input, doCmd, i))
            {
                enabled = true;
                i += doCmd.Length;
                continue;
            }


            if (enabled)
            {
                var m = r.Match(input, i);
                if (m.Success)
                {
                    var a = int.Parse(m.Groups[1].Value, NumberStyles.Integer);
                    var b = int.Parse(m.Groups[2].Value, NumberStyles.Integer);
                    result += a * b;
                    i += m.Length;
                    continue;
                }
            }

            // No match or command found, move to next character
            i++;
        }

        return result.ToString();
    }

    private bool IsCmdAtIndex(string input, string cmd, int index) => index + cmd.Length <= input.Length && input.Substring(index, cmd.Length) == cmd;
}
