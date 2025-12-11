using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D11;

[PuzzleInfo(year: 2025, day: 11, part: 1, title: "Reactor")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var nodes = InputParser.Parse(Input);
        var paths = PathFinder.DFS(nodes, nodes["you"], nodes["out"]);
        return paths.ToString();
    }
}
