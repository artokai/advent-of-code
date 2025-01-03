using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D09;

public static class InputParser
{
    public static Dictionary<string, List<(string Target, int Distance)>> ParseInput(PuzzleInput input)
    {
        var re = new Regex(@"(?<from>\w+) to (?<to>\w+) = (?<distance>\d+)");
        var edges = input.AsLines()
            .Select(line => re.Match(line))
            .Where(m => m.Success)
            .Select(m => (Start: m.Groups["from"].Value, Edge: (Target: m.Groups["to"].Value, Distance: int.Parse(m.Groups["distance"].Value))))
            .GroupBy(x => x.Start)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Edge).ToList()
            );

        var edgeKeys = edges.Keys.ToList();
        foreach (var city in edgeKeys)
        {
            foreach (var edge in edges[city])
            {
                if (!edges.ContainsKey(edge.Target))
                {
                    edges[edge.Target] = new List<(string Target, int Distance)>();
                }
                edges[edge.Target].Add((city, edge.Distance));
            }
        }
        return edges;
    }
}