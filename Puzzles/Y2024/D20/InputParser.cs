using Artokai.AOC.Core.Geometry;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D20;

public static class InputParser
{
    public static (char[,] map, Vector2DInt start, Vector2DInt end) ParseInput(PuzzleInput input)
    {
        var lines = input.AsLines();
        var start = new Vector2DInt(0, 0);
        var end = new Vector2DInt(0, 0);
        char[,] map = new char[lines[0].Length, lines.Count];
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                map[x, y] = lines[y][x] == '#' ? '#' : '.';
                if (lines[y][x] == 'S') { start = new Vector2DInt(x, y); }
                if (lines[y][x] == 'E') { end = new Vector2DInt(x, y); }
            }
        }
        return (map, start, end);
    }
}
