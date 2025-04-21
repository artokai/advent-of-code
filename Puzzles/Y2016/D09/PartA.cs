using System.Text;
using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D09;

[PuzzleInfo(year: 2016, day: 9, part: 1, title: "Explosives in Cyberspace")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var re = new Regex(@"(?<text>[^()]+)|\((?<len>\d+)x(?<cnt>\d+)\)");

        var index = 0;
        var match = re.Match(input, index);
        var sb = new StringBuilder();
        while (match.Success) {
            if (match.Groups["text"].Success)
            {
                sb.Append(match.Groups["text"].Value);
                index += match.Length;
            }
            else
            {
                var blockLength = int.Parse(match.Groups["len"].Value);
                var repeatCount = int.Parse(match.Groups["cnt"].Value);
                var text = input.Substring(index + match.Length, blockLength);
                for (var i = 0; i < repeatCount; i++)
                {
                    sb.Append(text);
                }
                index += match.Length + blockLength;
            }
            match = re.Match(input, index);
        }
        return sb.Length.ToString();
    }
}
