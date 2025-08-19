using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D12;

[PuzzleInfo(year: 2017, day: 12, part: 1, title: "Digital Plumber")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var connections = InputParser.Parse(Input);
        var visitor = new Visitor();
        visitor.Visit(0, connections);
        return visitor.Visited.Count.ToString();
    }
}
