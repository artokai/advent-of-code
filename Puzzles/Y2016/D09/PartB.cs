using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D09;

[PuzzleInfo(year: 2016, day: 9, part: 2, title: "Explosives in Cyberspace")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var size = GetSize(input);
        return size.ToString();
    }

    private long GetSize(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0L;
        }

        var re = new Regex(@"(?<text>[^()]+)|\((?<len>\d+)x(?<cnt>\d+)\)");
        var match = re.Match(input);
        if (!match.Success)
        {
            throw new ArgumentException("Invalid input format!");
        }
        if (match.Groups["text"].Success)
        {
            var size = match.Groups["text"].Length;
            var restSize = GetSize(input.Substring(match.Index + match.Length));
            return size + restSize;
        }
        else
        {
            var blockLength = int.Parse(match.Groups["len"].Value);
            var repeatCount = int.Parse(match.Groups["cnt"].Value);
            var size = repeatCount * GetSize(input.Substring(match.Index + match.Length, blockLength));
            var restSize = GetSize(input.Substring(match.Index + match.Length + blockLength));
            return size + restSize;
        }
    }
}
