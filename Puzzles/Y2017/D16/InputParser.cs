using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2017.D16;

public static class InputParser
{
    public static List<Func<char[], char[]>> Parse(PuzzleInput input)
    {
        return input
            .AsSingleLine()
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(ParseMove)
            .ToList();
    }

    private static Func<char[], char[]> ParseMove(string move)
    {
        var cmd = move[0];
        var parameters = move.Substring(1).Split('/').ToArray();
        return cmd switch
        {
            's' => dancers => Spin(dancers, int.Parse(parameters[0])),
            'x' => dancers => Exchange(dancers, int.Parse(parameters[0]), int.Parse(parameters[1])),
            'p' => dancers => Partner(dancers, parameters[0][0], parameters[1][0]),
            _ => throw new ArgumentException($"Invalid move: {move}")
        };
    }

    public static char[] Spin(char[] dancers, int count)
    {
        var totalCount = dancers.Length;
        var result = dancers.Skip(totalCount - count).Take(count).Concat(dancers.Take(totalCount - count)).ToArray();
        return result;
    }

    private static char[] Exchange(char[] dancers, int indexA, int indexB)
    {
        var result = new char[dancers.Length];
        Array.Copy(dancers, result, dancers.Length);
        result[indexA] = dancers[indexB];
        result[indexB] = dancers[indexA];
        return result;
    }

    private static char[] Partner(char[] dancers, char dancerA, char dancerB)
    {
        var indexA = Array.IndexOf(dancers, dancerA);
        var indexB = Array.IndexOf(dancers, dancerB);
        return Exchange(dancers, indexA, indexB);
    }
}
