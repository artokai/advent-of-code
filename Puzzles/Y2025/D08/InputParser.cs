using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2025.D08;

public class InputParser
{
    public static (List<Graph> graphs, List<Edge> potentialEdges) Parse(PuzzleInput input, int? maxEdges = null)
    {
        var nodes = input.AsLines()
            .Select(line =>
            {
                var parts = line.Split(",", StringSplitOptions.TrimEntries);
                var node = new Node
                {
                    X = long.Parse(parts[0]),
                    Y = long.Parse(parts[1]),
                    Z = long.Parse(parts[2]),
                    Graph = new Graph()
                };
                node.Graph.Nodes.Add(node);
                return node;
            }).ToList();

        var graphs = nodes.Select(n => n.Graph).ToList();

        var edgesQuery = nodes.SelectMany((a, index) =>
            nodes.Skip(index + 1).Select(b => new Edge { A = a, B = b })
        )
        .OrderBy(e => e.DistanceSqueared);

        var edgesToConnect = maxEdges.HasValue
            ? edgesQuery.Take(maxEdges.Value).ToList()
            : edgesQuery.ToList();

        return (graphs, edgesToConnect);
    }
}
