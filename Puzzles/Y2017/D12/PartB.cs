using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D12;

[PuzzleInfo(year: 2017, day: 12, part: 2, title: "Digital Plumber")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var connections = InputParser.Parse(Input);
        var visitor = new Visitor();
        var groupCount = 0;
        foreach (var id in connections.Keys)
        {
            if (!visitor.HasVisited(id))
            {
                groupCount++;
                visitor.Visit(id, connections);
            }
        }
        return groupCount.ToString();
    }
}
