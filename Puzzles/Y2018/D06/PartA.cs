using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2018.D06;

[PuzzleInfo(year: 2018, day: 6, part: 1, title: "Chronal Coordinates")]
public class PartA : SolverBase
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
        var merged = MergeMaps(maps, width, height);
        var sizes = CalculateSizes(merged, width, height);
        var result = sizes.Values.Max();
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

    private int[] MergeMaps(List<int[]> maps, int width, int height)
    {
        var merged = new int[width * height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var minOwner = -1;
                var minDistance = int.MaxValue;
                var mapIndex = y * width + x;

                for (var i = 0; i < maps.Count; i++)
                {
                    var curDistance = maps[i][mapIndex];
                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        minOwner = i;
                    }
                    else if (curDistance == minDistance)
                    {
                        minOwner = -1;
                    }
                }
                merged[mapIndex] = minOwner;
            }
        }
        return merged;
    }

    private Dictionary<int, int> CalculateSizes(int[] merged, int width, int height)
    {
        var sizes = new Dictionary<int, int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var coordIndex = merged[y * width + x];
                if (coordIndex < 0) { continue; }
                if (!sizes.ContainsKey(coordIndex))
                {
                    sizes.Add(coordIndex, 0);
                }

                var onEdge = x <= 0 || y <= 0 || x >= width - 1 || y >= height - 1;
                var newSize = sizes[coordIndex] == int.MinValue || onEdge ? int.MinValue : sizes[coordIndex] + 1;
                sizes[coordIndex] = newSize;
            }
        }
        return sizes;
    }
}
