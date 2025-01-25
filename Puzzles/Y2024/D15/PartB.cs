using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D15;

[PuzzleInfo(year: 2024, day: 15, part: 2, title: "Warehouse Woes")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (inputMap, startPos, commands) = InputParser.Parse(Input);
        var map = ConvertMap(inputMap);
        var position = new Vector2DInt(startPos.X * 2, startPos.Y);
        var iter = 0;
        while (commands.Count > 0)
        {
            var direction = commands.Dequeue();
            var newPos = position + direction;
            if (CanPush(map, newPos, direction))
            {
                Push(map, newPos, direction);

                // Move robot
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
                if (map[x, y] == '[')
                {
                    sum += 100 * y + x;
                }
            }
        }
        return sum.ToString();
    }

    private bool CanPush(char[,] map, Vector2DInt pos, Vector2DInt dir)
    {
        if (map[pos.X, pos.Y] == '.')
        {
            return true;
        }

        if (map[pos.X, pos.Y] == '#')
        {
            return false;
        }

        if (dir.Y != 0)
        {
            if (map[pos.X, pos.Y] == '[' || map[pos.X, pos.Y] == ']')
            {
                // This will result same checks to be done multiple times if the box above is 
                // directly inline with  us but it's fine for now
                var otherDelta = map[pos.X, pos.Y] == '[' ? Vector2DInt.Right : Vector2DInt.Left;
                return CanPush(map, pos + dir, dir) && CanPush(map, pos + otherDelta + dir, dir);
            }
        }

        if (dir.X != 0)
        {
            return CanPush(map, pos + dir, dir);
        }

        Console.WriteLine($"Should not be checking CanPush for {map[pos.X, pos.Y]}");
        return false;
    }

    private void Push(char[,] map, Vector2DInt pos, Vector2DInt dir)
    {
        if (map[pos.X, pos.Y] == '.')
        {
            return;
        }

        if (map[pos.X, pos.Y] == '#')
        {
            return;
        }

        if (dir.Y != 0)
        {
            if (map[pos.X, pos.Y] == '[' || map[pos.X, pos.Y] == ']')
            {
                // Push both blocks above the box
                var otherDelta = map[pos.X, pos.Y] == '[' ? Vector2DInt.Right : Vector2DInt.Left;
                Push(map, pos + dir, dir);
                Push(map, pos + otherDelta + dir, dir);

                // Move both boxes
                var targetPos = pos + dir;
                map[targetPos.X, targetPos.Y] = map[pos.X, pos.Y];
                map[pos.X, pos.Y] = '.';

                var otherTarget = targetPos + otherDelta;
                var otherPos = pos + otherDelta;
                map[otherTarget.X, otherTarget.Y] = map[otherPos.X, otherPos.Y];
                map[otherPos.X, otherPos.Y] = '.';
            }
        }

        if (dir.X != 0)
        {
            Push(map, pos + dir, dir);
            var targetPos = pos + dir;
            map[targetPos.X, targetPos.Y] = map[pos.X, pos.Y];
            map[pos.X, pos.Y] = '.';
        }
    }

    private char[,] ConvertMap(char[,] inputMap)
    {
        var map = new char[inputMap.GetLength(0) * 2, inputMap.GetLength(1)];
        for (var y = 0; y < inputMap.GetLength(1); y++)
        {
            for (var x = 0; x < inputMap.GetLength(0); x++)
            {
                var c = inputMap[x, y];
                if (c == '#')
                {
                    map[x * 2, y] = '#';
                    map[x * 2 + 1, y] = '#';
                    continue;
                }
                if (c == '.')
                {
                    map[x * 2, y] = '.';
                    map[x * 2 + 1, y] = '.';
                    continue;
                }
                if (c == 'O')
                {
                    map[x * 2, y] = '[';
                    map[x * 2 + 1, y] = ']';
                    continue;
                }
                if (c == '@')
                {
                    map[x * 2, y] = '@';
                    map[x * 2 + 1, y] = '.';
                    continue;
                }
            }
        }
        return map;
    }
}
