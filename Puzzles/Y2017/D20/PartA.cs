using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D20;

[PuzzleInfo(year: 2017, day: 20, part: 1, title: "Particle Swarm")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        // In the long-term, the particle with the smallest acceleration
        // will stay closest to <0,0,0>.
        var re = new Regex("<(?<x>-?\\d+),(?<y>-?\\d+),(?<z>-?\\d+)>");
        var id = Input.AsLines()
            .Select((line, index) =>
            {
                var matches = re.Matches(line);
                if (matches.Count != 3)
                    throw new InvalidOperationException($"Line is not valid: '{line}'");
                var x = int.Parse(matches[2].Groups["x"].Value);
                var y = int.Parse(matches[2].Groups["y"].Value);
                var z = int.Parse(matches[2].Groups["z"].Value);
                return (Id: index, LengthSquared: x * x + y * y + z * z);
            })
            .MinBy(g => g.LengthSquared)
            .Id;
        return id.ToString();
    }
}
