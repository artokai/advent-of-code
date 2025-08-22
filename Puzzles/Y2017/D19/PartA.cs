using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D19;

[PuzzleInfo(year: 2017, day: 19, part: 1, title: "A Series of Tubes")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (grid, startPosition, pois) = InputParser.Parse(Input);
        var packet = new Packet(startPosition, grid, pois);
        packet.Traverse();
        return packet.Collected.Aggregate("", (acc, cur) => acc + cur.Id);
    }
}
