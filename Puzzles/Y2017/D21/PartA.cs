using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D21;

[PuzzleInfo(year: 2017, day: 21, part: 1, title: "Fractal Art")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var current = Grid.Parse(".#./..#/###");
        var patterns = InputParser.Parse(Input);
        var enhancer = new Enhancer(patterns);
        for (var i = 0; i < 5; i++)
        {
            current = enhancer.Enhance(current);
        }

        return current.CountActiveCells().ToString();
    }
}
