using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D18;

[PuzzleInfo(year: 2015, day: 18, part: 1, title: "Like a GIF For Your Yard")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var map = Input.AsCharTable();
        var simulation = new Simulation(map);
        for (var i = 0; i < 100; i++)
        {
            simulation.Update();
        }
        return simulation.GetActiveCount().ToString();
    }
}
