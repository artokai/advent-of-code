using Artokai.AOC.Core.Geometry;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2017.D19;

public static class InputParser
{
    public static (MapData grid, Vector2DInt startPosition, List<POI> pois) Parse(PuzzleInput input)
    {
        var lines = input.AsLines();
        var width = lines.Max(line => line.Length);
        var height = lines.Count;

        var map = new bool[width, height];
        Vector2DInt startPosition = new(-1, -1);
        List<POI> pois = new();
        for (int y = 0; y < height; y++)
        {
            var line = lines[y];
            for (int x = 0; x < width; x++)
            {
                map[x, y] = line[x] != ' ';
                if (startPosition.X < 0 && map[x, y])
                {
                    startPosition = new Vector2DInt(x, y);
                }
                if (line[x] >= 'A' && line[x] <= 'Z')
                {
                    pois.Add(new POI(line[x], new Vector2DInt(x, y)));
                }
            }
        }

        return (new MapData(map, width, height), startPosition, pois);
    }
}

public record MapData(bool[,] Grid, int Width, int Height);

public record class POI(char Id, Vector2DInt Location);
