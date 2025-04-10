using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D18;

[PuzzleInfo(year: 2015, day: 18, part: 2, title: "Like a GIF For Your Yard")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var map = Input.AsCharTable();
        var width = map.GetLength(0);
        var height = map.GetLength(1);

        var simulation = new Simulation(map);
        for (var i = 0; i < 100; i++)
        {
            simulation.Update();
            simulation.Set(0, 0, '#');
            simulation.Set(0, height - 1, '#');
            simulation.Set(width - 1, height - 1, '#');
            simulation.Set(width - 1, 0, '#');
        }
        return simulation.GetActiveCount().ToString();
    }
}
