using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D07;

[PuzzleInfo(year: 2018, day: 7, part: 1, title: "The Sum of Its Parts")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var nodes = InputParser.Parse(Input);
        var nodeCount = nodes.Count;
        var selected = new List<Node>();
        while (selected.Count < nodeCount)
        {
            var next = nodes
                .Where(n => n.Predecessors.Count <= 0)
                .OrderBy(n => n.Key)
                .First();

            nodes.Remove(next);
            selected.Add(next);
            foreach (var dependent in next.Dependents)
            {
                dependent.Predecessors.Remove(next);
            }
        }

        var result = new string(selected.Select(n => n.Key).ToArray());
        return result;
    }
}
