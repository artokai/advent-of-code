using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2015.D03;

[PuzzleInfo(year: 2015, day: 3, part: 2, title: "Perfectly Spherical Houses in a Vacuum")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        List<IEnumerable<char>> lists = [
            input.Where((_c, i) => i % 2 == 0),
            input.Where((_c, i) => i % 2 != 0)
        ];

        var visited = new Dictionary<Vector2DInt, int>();
        foreach (var list in lists)
        {
            var pos = new Vector2DInt(0, 0);
            visited[pos] = visited.GetValueOrDefault(pos, 0) + 1;
            foreach (var c in list)
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
        }

        var result = visited.Where(kp => kp.Value > 0).Count();
        return result.ToString();
    }
}
