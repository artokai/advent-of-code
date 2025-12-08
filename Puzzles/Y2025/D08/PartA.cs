using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D08;

[PuzzleInfo(year: 2025, day: 8, part: 1, title: "Playground")]
public class PartA : SolverBase
{
    private const int MAX_EDGES = 1_000;
    private const int GRAPHS_TO_INCLUDE_IN_RESULT = 3;

    public override string Solve()
    {
        var (graphs, edges) = InputParser.Parse(Input, MAX_EDGES);
        foreach (var edge in edges)
        {
            edge.A.Graph.Merge(graphs, edge.B.Graph);
            edge.A.Graph.Edges.Add(edge);
        }

        var result = graphs
            .OrderByDescending(g => g.Nodes.Count)
            .Take(GRAPHS_TO_INCLUDE_IN_RESULT)
            .Select(g => g.Nodes.Count)
            .Aggregate(1L, (result, size) => result * size);

        return result.ToString();
    }
}
