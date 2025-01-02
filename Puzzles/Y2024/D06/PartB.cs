using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D06;

[PuzzleInfo(year: 2024, day: 6, part: 2, title: "Guard Gallivant")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var (map, guard) = InputParser.ParseInput(lines);
        var initialSim = new Simulation(map, guard);
        initialSim.TrackEntries = true;
        initialSim.Run();

        var result = 0L;
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        var pathPositions = initialSim.GetEntries();
        Parallel.ForEach(pathPositions, parallelOptions, pos =>
        {
            var dir = initialSim.Entries[pos.X + pos.Y * map.Width];
            if (RunSimulation(map, pos, dir).IsRunningInCircles) { Interlocked.Increment(ref result); }
        });
        return result.ToString();
    }

    public Simulation RunSimulation(Map map, Vector2DInt obstaclePos, byte directionByte)
    {
        var sim = new Simulation(map, obstaclePos, directionByte);
        sim.TrackEntries = false;
        sim.Run();
        return sim;
    }
}
