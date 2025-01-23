using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D18;

[PuzzleInfo(year: 2024, day: 18, part: 2, title: "RAM Run")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var obstacles = Input.AsPairs<int, int>()
            .Select(pair => new Coordinate(pair.Item1, pair.Item2))
            .ToList();

        var start = new Coordinate(0, 0);
        var end = new Coordinate(70, 70);

        var lastSolvable = 0;
        var min = 0;
        var max = obstacles.Count - 1;

        var pathFinder = new PathFinder([], 71);
        while (min <= max)
        {
            var mid = (min + max) / 2;
            pathFinder.Reset(obstacles.Take(mid).ToList());
            if (pathFinder.FindPath(start, end) > 0)
            {
                lastSolvable = mid;
                min = mid + 1;
            }
            else
            {
                max = mid - 1;
            }
        }

        var lastObstacle = obstacles[lastSolvable];
        return $"{lastObstacle.X},{lastObstacle.Y}";
    }
}
