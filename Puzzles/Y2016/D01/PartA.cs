using System.Numerics;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D01;

[PuzzleInfo(year: 2016, day: 1, part: 1, title: "No Time for a Taxicab")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var moves = Input.AsSingleLine()
            .Split(", ")
            .Select(x => (x[0], int.Parse(x[1..])));

        var position = new Vector2DInt(0, 0);
        var direction = Vector2DInt.Up;
        foreach (var (turn, steps) in moves)
        {
            direction = turn switch
            {
                'L' => direction.TurnLeft(),
                'R' => direction.TurnRight(),
                _ => throw new ArgumentOutOfRangeException()
            };
            position += direction * steps;
        }

        return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
    }
}
