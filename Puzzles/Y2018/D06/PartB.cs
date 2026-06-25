using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2018.D06;

[PuzzleInfo(year: 2018, day: 6, part: 2, title: "Chronal Coordinates")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var coords = Input.AsLines()
            .Select(line => line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(pair => new Vector2DInt(int.Parse(pair[0]), int.Parse(pair[1])))
            .ToList();
        var width = coords.Aggregate(int.MinValue, (acc, cur) => Math.Max(acc, cur.X)) + 1;
        var height = coords.Aggregate(int.MinValue, (acc, cur) => Math.Max(acc, cur.Y)) + 1;

        var maps = coords.Select(c => CreateDistanceMap(c, width, height)).ToList();
        var totals = CreateTotalsMap(maps, width, height);
        var result = totals.Count(d => d < 10000);
        return result.ToString();
    }

    private int[] CreateDistanceMap(Vector2DInt coord, int width, int height)
    {
        var map = new int[height * width];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                map[y * width + x] = Math.Abs(coord.X - x) + Math.Abs(coord.Y - y);
            }
        }
        return map;
    }

    private int[] CreateTotalsMap(List<int[]> maps, int width, int height)
    {
        var totals = new int[width * height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var mapIndex = y * width + x;
                var total = maps.Aggregate(0, (acc, cur) => acc + cur[mapIndex]);
                totals[mapIndex] = total;
            }
        }
        return totals;
    }
}
