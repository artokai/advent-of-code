using System.Runtime.CompilerServices;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D02;

[PuzzleInfo(year: 2015, day: 2, part: 1, title: "I Was Told There Would Be No Math")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var total = 0L;
        var lines = Input.AsLines();
        foreach (var line in lines)
        {
            // l * w * h
            var dimensions = line.Split('x').Select(int.Parse).ToArray();
            var lw = dimensions[0] * dimensions[1];
            var lh = dimensions[0] * dimensions[2];
            var wh = dimensions[1] * dimensions[2];
            var surface = 2 * lw + 2 * lh + 2 * wh;
            var extra = Math.Min(lw, Math.Min(lh, wh));
            total += surface + extra;
        }
        return total.ToString();
    }
}
