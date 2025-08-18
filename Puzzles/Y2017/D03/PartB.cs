using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D03;

[PuzzleInfo(year: 2017, day: 3, part: 2, title: "Spiral Memory")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var target = int.Parse(Input.AsSingleLine());
        Dictionary<Vector2DInt, int> grid = new();

        var current = 1;
        var stepsPerSide = 1;
        var pos = new Vector2DInt(0, 0);
        grid[pos] = current;

        var steps = 0;
        var dir = Vector2DInt.Right;
        while (current < target)
        {
            steps++;
            pos += dir;
            current = SumNeighbours(grid, pos);
            grid[pos] = current;

            if (steps % stepsPerSide == 0)
            {
                dir = dir.TurnLeft();
                if (dir == Vector2DInt.Right)
                {
                    stepsPerSide++;
                }
                if (dir == Vector2DInt.Left)
                {
                    stepsPerSide++;
                }
            }
        }

        return current.ToString();
    }

    private int SumNeighbours(Dictionary<Vector2DInt, int> grid, Vector2DInt pos)
    {
        return
            GetNeighbourValue(grid, pos + new Vector2DInt(-1, -1)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(0, -1)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(1, -1)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(-1, 0)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(1, 0)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(-1, 1)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(0, 1)) +
            GetNeighbourValue(grid, pos + new Vector2DInt(1, 1));
    }

    private int GetNeighbourValue(Dictionary<Vector2DInt, int> grid, Vector2DInt pos)
    {
        return grid.TryGetValue(pos, out var value) ? value : 0;
    }
}
