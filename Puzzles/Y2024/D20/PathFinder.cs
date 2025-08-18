using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D20;

public static class PathFinder
{
    public static (List<Vector2DInt>, Dictionary<Vector2DInt, int>) Find(char[,] map, Vector2DInt start, Vector2DInt end)
    {
        var stepMap = new Dictionary<Vector2DInt, int>();
        var path = new List<Vector2DInt>();

        var steps = 0;
        var pos = start;

        while (pos != end)
        {
            stepMap[pos] = steps;
            path.Add(pos);

            if (map[pos.X, pos.Y - 1] == '.' && !stepMap.ContainsKey(new Vector2DInt(pos.X, pos.Y - 1))) { pos = new Vector2DInt(pos.X, pos.Y - 1); }
            else if (map[pos.X + 1, pos.Y] == '.' && !stepMap.ContainsKey(new Vector2DInt(pos.X + 1, pos.Y))) { pos = new Vector2DInt(pos.X + 1, pos.Y); }
            else if (map[pos.X, pos.Y + 1] == '.' && !stepMap.ContainsKey(new Vector2DInt(pos.X, pos.Y + 1))) { pos = new Vector2DInt(pos.X, pos.Y + 1); }
            else if (map[pos.X - 1, pos.Y] == '.' && !stepMap.ContainsKey(new Vector2DInt(pos.X - 1, pos.Y))) { pos = new Vector2DInt(pos.X - 1, pos.Y); }
            else
            {
                Console.WriteLine("No path found");
                Console.WriteLine($"Steps: {steps}");
                Console.WriteLine($"Pos: {pos.X}, {pos.Y}");
                Console.WriteLine($"End: {end.X}, {end.Y}");
                Environment.Exit(1);
            }

            steps++;
        }
        stepMap[end] = steps;
        path.Add(pos);

        return (path, stepMap);
    }
}
