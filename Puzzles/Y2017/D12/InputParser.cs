using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2017.D12;

public static class InputParser
{
    public static Dictionary<int, List<int>> Parse(PuzzleInput input)
    {
        var lines = input.AsLines();
        var connections = new Dictionary<int, List<int>>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var key = int.Parse(parts[0]);
            var values = parts
                .Skip(2)
                .Select(s => s.TrimEnd(','))
                .Select(int.Parse)
                .ToList();
            connections[key] = values;
        }
        return connections;
    }
}
