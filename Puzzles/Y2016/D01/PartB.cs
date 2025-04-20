using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D01;

[PuzzleInfo(year: 2016, day: 1, part: 2, title: "No Time for a Taxicab")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var moves = Input.AsSingleLine()
            .Split(", ")
            .Select(x => (x[0], int.Parse(x[1..])));

        var position = new Vector2DInt(0, 0);
        var direction = Vector2DInt.Up;
        var visited = new HashSet<Vector2DInt>() { position };
        foreach (var (turn, steps) in moves)
        {
            direction = turn switch
            {
                'L' => direction.TurnLeft(),
                'R' => direction.TurnRight(),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Move step by step to check for intersections
            for (int i = 0; i < steps; i++)
            {
                position += direction;
                if (visited.Contains(position))
                {
                    return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
                }
                visited.Add(position);
            }
        }

        return "No intersection found!";
    }
}
