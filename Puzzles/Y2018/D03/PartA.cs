using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D03;

[PuzzleInfo(year: 2018, day: 3, part: 1, title: "No Matter How You Slice It")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var claims = InputParser.Parse(Input);

        var fabric = new int[1000 * 1000];
        foreach (var claim in claims) {
            for (var dx = 0; dx < claim.Width; dx++) {
                for (var dy = 0; dy < claim.Height; dy++) {
                    var i = (1000 * (claim.Y + dy)) + claim.X + dx;
                    fabric[i] +=1;
                }
            }
        }

        return fabric.Count(x => x > 1).ToString();
    }
}
