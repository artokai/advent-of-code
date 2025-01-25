using Artokai.AOC.Core.Geometry;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D15;

public static class InputParser
{
    public static (char[,] map, Vector2DInt startPos, Queue<Vector2DInt> commands) Parse(PuzzleInput input)
    {
        var inputParts = input.SplitOnEmptyLines();
        var map = inputParts[0].AsCharTable();
        var startPos = FindStartPosition(map);
        var commands = ParseCommands(inputParts[1]);
        return (map, startPos, commands);
    }

    private static Queue<Vector2DInt> ParseCommands(PuzzleInput input)
    {
        var commands = input.AsSingleLine().ToCharArray().Select(c => c switch
             {
                 '<' => Vector2DInt.Left,
                 '>' => Vector2DInt.Right,
                 '^' => Vector2DInt.Up,
                 'v' => Vector2DInt.Down,
                 _ => throw new Exception($"Invalid command: {c}")
             }
        );
        return new Queue<Vector2DInt>(commands);
    }

    private static Vector2DInt FindStartPosition(char[,] map)
    {
        for (var x = 0; x < map.GetLength(0); x++)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == '@') return new Vector2DInt(x, y);
            }
        }
        throw new Exception("Start position not found in input!");
    }
}
