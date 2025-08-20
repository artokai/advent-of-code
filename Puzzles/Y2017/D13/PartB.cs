using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D13;

[PuzzleInfo(year: 2017, day: 13, part: 2, title: "Packet Scanners")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var layers = Input.AsLines()
            .Select(Layer.Parse)
            .ToList();

        var delay = 10;
        while (layers.Any(layer => layer.IsAtTopWhenLeavingAt(delay)))
            delay++;

        return delay.ToString();
    }
}
