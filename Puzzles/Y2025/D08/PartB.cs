using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D08;

[PuzzleInfo(year: 2025, day: 8, part: 2, title: "Playground")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (graphs, edges) = InputParser.Parse(Input, null);
        Edge? edge = null;
        var index = 0;
        while (graphs.Count > 1 && index < edges.Count)
        {
            edge = edges[index];
            edge.A.Graph.Merge(graphs, edge.B.Graph);
            edge.A.Graph.Edges.Add(edge);
            index++;
        }

        return (graphs.Count == 1 && edge != null)
            ? (edge.A.X * edge.B.X).ToString()
            : "No solution found!";
    }
}
