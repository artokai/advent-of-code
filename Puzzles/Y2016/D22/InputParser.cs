using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D22;

public static class InputParser
{
    public static IEnumerable<Drive> ParseInput(PuzzleInput input)
    {
        return input.AsLines().Skip(2).Select(ParseLine);
    }

    private static Drive ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var name = parts[0];
        var (x, y) = GetCoordinates(name);
        return new Drive(
            Name: name,
            X: x,
            Y: y,
            Size: int.Parse(parts[1].TrimEnd('T')),
            Used: int.Parse(parts[2].TrimEnd('T')),
            Available: int.Parse(parts[3].TrimEnd('T')),
            UsePercentage: int.Parse(parts[4].TrimEnd('%'))
        );
    }

    private static (int x, int y) GetCoordinates(string name)
    {
        var coordRe = new Regex(@"-x(?<x>\d+)-y(?<y>\d+)");
        var match = coordRe.Match(name);
        if (!match.Success)
        {
            throw new FormatException($"Invalid coordinate format in name: {name}");
        }

        int x = int.Parse(match.Groups["x"].Value);
        int y = int.Parse(match.Groups["y"].Value);
        return (x, y);
    }
}

public record Drive(string Name, int X, int Y, int Size, int Used, int Available, int UsePercentage);
