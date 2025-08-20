using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D13;

[PuzzleInfo(year: 2017, day: 13, part: 1, title: "Packet Scanners")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var layers = Input.AsLines()
            .Select(Layer.Parse)
            .ToList();

        var severity = layers
            .Where(l => l.IsAtTopWhenLeavingAt(0))
            .Aggregate(0, (acc, layer) => acc + layer.Depth * layer.Range);

        return severity.ToString();
    }
}
