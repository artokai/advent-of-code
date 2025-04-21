using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D08;

[PuzzleInfo(year: 2016, day: 8, part: 1, title: "Two-Factor Authentication")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var map = new Map(50, 6);
        var instructions = Input.AsLines();
        foreach(var instruction in instructions) {
            var cmd = Command.Parse(instruction);
            cmd.Apply(map);
        }

        // Cast flattens the 2d array to a 1d enumerable
        return map.Pixels
            .Cast<bool>()
            .Count(p => p == true)
            .ToString();
    }
}
