using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2025.D11;

public record Node(string Name, List<string> Outputs);

public static class InputParser
{
    public static Dictionary<string, Node> Parse(PuzzleInput input)
    {
        var nodes = input
            .AsLists<string>([' ', ':'])
            .Select(l => new Node(
                Name: l[0],
                Outputs: l.Skip(1).ToList()))
            .ToDictionary(n => n.Name, n => n);

        // Ensure that all outnodes are included in nodes
        nodes.Values
            .SelectMany(n => n.Outputs)
            .Distinct()
            .Where(name => !nodes.ContainsKey(name))
            .ToList()
            .ForEach(name => nodes[name] = new Node(name, new List<string>()));

        if (nodes["out"].Outputs.Count > 0)
        {
            throw new Exception("The 'out' node should not have any outputs.");
        }

        return nodes;
    }
}
