using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2018.D10;

[PuzzleInfo(year: 2018, day: 10, part: 1, title: "The Stars Align")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (lights, size, _) = Solver.Solve(Input, 15_000);
        PrintLights(lights, size);
        return "<See output above>";
    }

    private void PrintLights(List<Vector2DInt> lights, Vector2DInt size)
    {
        for (var y = 0; y <= size.Y; y++)
        {
            for (var x = 0; x <= size.X; x++)
            {
                Console.Write(lights.Any(l => l.X == x && l.Y == y) ? '#' : ' ');
            }
            Console.WriteLine();
        }
    }
}
