using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D14;

[PuzzleInfo(year: 2024, day: 14, part: 2, title: "Restroom Redoubt")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var debug = false;
        var initial = InputParser.ParseInput(Input);
        var isTree = false;
        var iteration = 1;
        while (!isTree)
        {
            iteration++;
            var after = Simulation.MoveRobots(initial, iteration);
            isTree = IsTree(after);
            if (isTree && debug)
            {
                Simulation.PrintMap(after);
            }

            if (iteration > 1_000_000)
            {
                throw new Exception("Too many iterations.. Giving up...");
            }
        }
        return iteration.ToString();
    }

    private bool IsTree(List<Robot> robots)
    {
        // Try to find a horizontal line of 10 or more robots
        // This seems to find the hidden picture
        var map = new bool[Simulation.MAP_WIDTH, Simulation.MAP_HEIGHT];
        foreach (var r in robots)
        {
            map[r.x, r.y] = true;
        }

        for (var y = 0; y < Simulation.MAP_HEIGHT; y++)
        {
            for (var x = 1; x < Simulation.MAP_WIDTH; x++)
            {
                if (!map[x, y]) continue;

                var len = 1;
                while (x + len < Simulation.MAP_WIDTH && map[x + len, y]) { len++; }
                if (len >= 10) return true;
            }
        }

        return false;
    }
}
