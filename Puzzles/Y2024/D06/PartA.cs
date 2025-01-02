using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D06;

[PuzzleInfo(year: 2024, day: 6, part: 1, title: "Guard Gallivant")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var (map, guard) = InputParser.ParseInput(lines);
        var sim = new Simulation(map, guard);
        sim.TrackEntries = false;
        sim.Run();
        return sim.GetVisitedPositions().Count().ToString();
    }
}
