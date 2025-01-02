using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D10;

public static class InputParser
{
    public static int[,] ParseInput(PuzzleInput input)
    {
        var lines = input.AsLines();
        var width = lines[0].Length;
        var height = lines.Count;

        var map = new int[width, height];
        for (var y = 0; y < height; y++)
        {
            var line = lines[y];
            for (var x = 0; x < width; x++)
            {
                var val = line[x] == '.' ? -1 : int.Parse(line[x].ToString());
                map[x, y] = val;
            }
        }

        return map;
    }
}
