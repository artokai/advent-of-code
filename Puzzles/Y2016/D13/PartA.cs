using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D13;

[PuzzleInfo(year: 2016, day: 13, part: 1, title: "A Maze of Twisty Little Cubicles")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var solver = new Solver(
            int.Parse(Input.AsSingleLine()),
            new Vector2DInt(1, 1),
            new Vector2DInt(31, 39)
        );
        solver.Solve();
        return solver.StepsToTarget.ToString();
    }
}
