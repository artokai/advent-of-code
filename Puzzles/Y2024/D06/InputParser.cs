namespace Artokai.AOC.Puzzles.Y2024.D06;

public static class InputParser
{
    public static (Map, Guard) ParseInput(List<string> lines)
    {
        var guard = new Guard(0, 0, '<');
        var width = lines[0].Length;
        var height = lines.Count;

        var map = new bool[width * height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] == '#')
                {
                    map[y * width + x] = true;
                }
                else if (lines[y][x] == '<' || lines[y][x] == '>' || lines[y][x] == 'v' || lines[y][x] == '^')
                {
                    guard = new Guard(x, y, lines[y][x]);
                }
            }
        }

        return (new Map(map, width, height), guard);
    }
}
