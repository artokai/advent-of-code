using Artokai.AOC.Core.Geometry;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2017.D22;

public static class InputParser
{
    public static HashSet<Vector2DInt> Parse(PuzzleInput input)
    {
        var lines = input.AsLines();
        var width = lines[0].Length;
        var height = lines.Count;

        var infected = new HashSet<Vector2DInt>();
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var cell = lines[y][x];
                if (cell == '#')
                {
                    infected.Add(new Vector2DInt(x - width / 2, y - height / 2));
                }
            }
        }
        return infected;
    }
}