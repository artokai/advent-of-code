using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D10;

[PuzzleInfo(year: 2024, day: 10, part: 1, title: "Hoof It")]
public class PartA : SolverBase
{
    private bool debug = false;
    public override string Solve()
    {
        var map = InputParser.ParseInput(Input);
        int result = 0;
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == 0)
                {
                    DebugLog("Found start at " + x + "," + y);
                    result += FindPath(map, x, y).Count;
                }
            }
        }
        return result.ToString();
    }

    private HashSet<string> FindPath(int[,] map, int x, int y, int indent = 0)
    {
        var current = map[x, y];
        if (current == 9)
        {
            return new HashSet<string> { $"{x},{y}" };
        }

        var routeCnt = 0;
        routeCnt += (x + 1 < map.GetLength(0) && map[x + 1, y] == current + 1) ? 1 : 0;
        routeCnt += (x - 1 >= 0 && map[x - 1, y] == current + 1) ? 1 : 0;
        routeCnt += (y + 1 < map.GetLength(1) && map[x, y + 1] == current + 1) ? 1 : 0;
        routeCnt += (y - 1 >= 0 && map[x, y - 1] == current + 1) ? 1 : 0;

        if (routeCnt > 1)
        {
            indent++;
        }
        var prefix = new string(' ', indent * 2);

        var result = new HashSet<string>();
        if (x + 1 < map.GetLength(0) && map[x + 1, y] == current + 1)
        {
            DebugLog(prefix + (x) + "," + y + " -> " + (x + 1) + "," + y + ": " + map[x, y] + "->" + map[x + 1, y]);
            result.UnionWith(FindPath(map, x + 1, y, indent));
        }
        if (x - 1 >= 0 && map[x - 1, y] == current + 1)
        {
            DebugLog(prefix + (x) + "," + y + " -> " + (x - 1) + "," + y + ": " + map[x, y] + "->" + map[x - 1, y]);
            result.UnionWith(FindPath(map, x - 1, y, indent));
        }
        if (y + 1 < map.GetLength(1) && map[x, y + 1] == current + 1)
        {
            DebugLog(prefix + (x) + "," + y + " -> " + (x) + "," + (y + 1) + ": " + map[x, y] + "->" + map[x, y + 1]);
            result.UnionWith(FindPath(map, x, y + 1, indent));
        }
        if (y - 1 >= 0 && map[x, y - 1] == current + 1)
        {
            DebugLog(prefix + (x) + "," + y + " -> " + (x) + "," + (y - 1) + ": " + map[x, y] + "->" + map[x, y - 1]);
            result.UnionWith(FindPath(map, x, y - 1, indent));
        }
        return result;
    }

    private void DebugLog(string message)
    {
        if (!debug) return;
        Console.WriteLine(message);
    }

    private void PrintMap(int[,] map)
    {
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y]);
            }
            Console.WriteLine();
        }
    }
}
