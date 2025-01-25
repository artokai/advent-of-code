using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D15;

[PuzzleInfo(year: 2024, day: 15, part: 1, title: "Warehouse Woes")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (map, startPos, commands) = InputParser.Parse(Input);
        var iter = 0;
        var position = startPos;
        while (commands.Count > 0)
        {
            var direction = commands.Dequeue();
            var newPos = position + direction;
            if (TryPush(map, newPos, direction))
            {
                map[position.X, position.Y] = '.';
                map[newPos.X, newPos.Y] = '@';
                position = newPos;
            }
            iter++;
        }

        long sum = 0;
        var w = map.GetLength(0);
        var h = map.GetLength(1);
        for (var y = 0; y < h; y++)
        {
            for (var x = 0; x < w; x++)
            {
                if (map[x, y] == 'O')
                {
                    sum += 100 * y + x;
                }
            }
        }

        return sum.ToString();
    }

    private bool TryPush(char[,] map, Vector2DInt pos, Vector2DInt dir)
    {
        if (map[pos.X, pos.Y] == '.')
        {
            return true;
        }

        if (map[pos.X, pos.Y] == '#')
        {
            return false;
        }

        var targetPos = pos + dir;
        if (TryPush(map, targetPos, dir))
        {
            map[targetPos.X, targetPos.Y] = map[pos.X, pos.Y];
            map[pos.X, pos.Y] = '.';
            return true;
        }

        return false;
    }
}
