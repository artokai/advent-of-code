using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D13;

[PuzzleInfo(year: 2016, day: 13, part: 2, title: "A Maze of Twisty Little Cubicles")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var maxSteps = 50;
        var solver = new Solver(
            int.Parse(Input.AsSingleLine()),
            new Vector2DInt(1, 1),
            null,
            maxSteps
        );
        solver.Solve();
        return solver.GetSeenCount().ToString();
    }
}
