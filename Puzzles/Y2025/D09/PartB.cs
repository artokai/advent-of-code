using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2025.D09;

using Corners = (int MinX, int MinY, int MaxX, int MaxY);

[PuzzleInfo(year: 2025, day: 9, part: 2, title: "Movie Theater")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var tiles = Input.AsLines()
            .Select(line =>
            {
                var parts = line.Split(',');
                return new Vector2DInt(int.Parse(parts[0]), int.Parse(parts[1]));
            })
            .ToList();

        // Condense the map to make search space smaller
        var (condensedTiles, xMap, yMap) = CondenseTiles(tiles);
        var map = CreateMap(condensedTiles, xMap.Count, yMap.Count);

        var maxSize = long.MinValue;
        foreach (var (a, b) in GetUniquePairs(condensedTiles))
        {
            // Get the size in original coordinate-space
            var size = GetUncondensedSize(a, b, xMap, yMap);
            if (size < maxSize)
            {
                continue;
            }

            // Check if rectangle is fully inside the polygon
            var corners = GetAllCorners(a, b);
            if (IsInside(map, corners))
            {
                maxSize = size;
            }
        }

        return maxSize.ToString();
    }

    private (List<Vector2DInt> condensedTiles, TwoWayDictionary<int, int> xMap, TwoWayDictionary<int, int> yMap) CondenseTiles(List<Vector2DInt> tiles)
    {

        var xMap = new TwoWayDictionary<int, int>();
        foreach (var mapEntry in tiles.Select(t => t.X).OrderBy(x => x).Distinct().Select((x, index) => (originalValue: x, mappedValue: index)))
        {
            xMap.Add(mapEntry.mappedValue, mapEntry.originalValue);
        }

        var yMap = new TwoWayDictionary<int, int>();
        foreach (var mapEntry in tiles.Select(t => t.Y).OrderBy(y => y).Distinct().Select((y, index) => (originalValue: y, mappedValue: index)))
        {
            yMap.Add(mapEntry.mappedValue, mapEntry.originalValue);
        }

        var condensedTiles = tiles
            .Select(t => new Vector2DInt(xMap.GetKey(t.X), yMap.GetKey(t.Y)))
            .ToList();

        return (condensedTiles, xMap, yMap);
    }

    private char[,] CreateMap(List<Vector2DInt> vertices, int width, int height)
    {
        // Create a new map and fill it with green inner tiles (x)
        // We use uppercase X for green border tiles
        // This allows us to flood fill the outside area later
        var map = new char[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                map[x, y] = 'x';
            }
        }

        var edges = new List<(Vector2DInt from, Vector2DInt to)>();
        for (var i = 0; i < vertices.Count; i++)
        {
            var start = vertices[i];
            var end = vertices[(i + 1) % vertices.Count];
            var edge = (from: start, to: end);
            edges.Add(edge);
        }

        // Fill in edges on map
        foreach (var edge in edges)
        {
            var from = edge.from;
            var to = edge.to;

            var xSign = Math.Sign(to.X - from.X);
            if (xSign != 0)
            {
                for (var x = from.X; x != to.X; x += xSign)
                {
                    map[x, from.Y] = 'X';
                }
            }

            var ySign = Math.Sign(to.Y - from.Y);
            if (ySign != 0)
            {
                for (var y = from.Y; y != to.Y; y += ySign)
                {
                    map[to.X, y] = 'X';
                }
            }

            map[from.X, from.Y] = '#';
            map[to.X, to.Y] = '#';
        }

        // Flood fill the outer area with empty space
        for (var x = 0; x < width; x++)
        {
            FloodFill(map, x, 0, 'x', '.');
            FloodFill(map, x, height - 1, 'x', '.');
        }
        for (var y = 0; y < height; y++)
        {
            FloodFill(map, 0, y, 'x', '.');
            FloodFill(map, width - 1, y, 'x', '.');
        }

        return map;
    }

    private void FloodFill(char[,] map, int x, int y, char fromChar, char toChar)
    {
        var mapWidth = map.GetLength(0);
        var mapHeight = map.GetLength(1);

        var q = new Stack<(int x, int y)>();
        q.Push((x, y));

        while (q.Count > 0)
        {
            var (cx, cy) = q.Pop();
            if (cx < 0 || cx >= mapWidth || cy < 0 || cy >= mapHeight)
                continue;
            if (map[cx, cy] != fromChar)
                continue;

            map[cx, cy] = toChar;
            q.Push((cx + 1, cy));
            q.Push((cx - 1, cy));
            q.Push((cx, cy + 1));
            q.Push((cx, cy - 1));
        }
    }

    private IEnumerable<(Vector2DInt a, Vector2DInt b)> GetUniquePairs(List<Vector2DInt> points)
    {
        for (var i = 0; i < points.Count - 1; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                yield return (points[i], points[j]);
            }
        }
    }

    private long GetUncondensedSize(Vector2DInt a, Vector2DInt b, TwoWayDictionary<int, int> xMap, TwoWayDictionary<int, int> yMap) =>
        (Math.Abs(xMap.GetValue(a.X) - xMap.GetValue(b.X)) + 1L) *
        (Math.Abs(yMap.GetValue(b.Y) - yMap.GetValue(a.Y)) + 1L);

    private Corners GetAllCorners(Vector2DInt a, Vector2DInt b) => (
        Math.Min(a.X, b.X),
        Math.Min(a.Y, b.Y),
        Math.Max(a.X, b.X),
        Math.Max(a.Y, b.Y)
    );

    private bool IsInside(char[,] map, Corners corners)
    {
        for (var x = corners.MinX; x <= corners.MaxX; x++)
        {
            for (var y = corners.MinY; y <= corners.MaxY; y++)
            {
                if (map[x, y] == '.')
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void PrintMap(char[,] map)
    {
        var mapWidth = map.GetLength(0);
        var mapHeight = map.GetLength(1);
        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                Console.Write(map[x, y]);
            }
            Console.WriteLine();
        }
    }
}
