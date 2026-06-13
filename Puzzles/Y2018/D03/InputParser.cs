using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2018.D03;

public record struct Claim(int Id, int X, int Y, int Width, int Height);

public static class InputParser
{
    public static List<Claim> Parse(PuzzleInput input)
    {
        var re = new Regex(@"^#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)$");
        return input.AsLines().Select((l) =>
            {
                var m = re.Match(l);
                return new Claim(
                    int.Parse(m.Groups["id"].Value),
                    int.Parse(m.Groups["x"].Value),
                    int.Parse(m.Groups["y"].Value),
                    int.Parse(m.Groups["w"].Value),
                    int.Parse(m.Groups["h"].Value)
                );
            }).ToList();
    }
}
