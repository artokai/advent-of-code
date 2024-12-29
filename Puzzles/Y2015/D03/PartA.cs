using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2015.D03;

[PuzzleInfo(year: 2015, day: 3, part: 1, title: "Perfectly Spherical Houses in a Vacuum")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var pos = new Vector2DInt(0, 0);
        var visited = new Dictionary<Vector2DInt, int> {
            { pos, 1 }
        };

        foreach (var c in input)
        {
            pos = c switch
            {
                '^' => pos + Vector2DInt.Up,
                'v' => pos + Vector2DInt.Down,
                '<' => pos + Vector2DInt.Left,
                '>' => pos + Vector2DInt.Right,
                _ => throw new Exception("Invalid input: " + c),
            };
            visited[pos] = visited.GetValueOrDefault(pos, 0) + 1;
        }

        var result = visited.Where(kp => kp.Value > 0).Count();
        return result.ToString();
    }
}
