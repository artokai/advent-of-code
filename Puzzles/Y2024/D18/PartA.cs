using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D18;

[PuzzleInfo(year: 2024, day: 18, part: 1, title: "RAM Run")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var obstacleCount = 1024;
        var obstacles = Input.AsPairs<int, int>()
            .Select(pair => new Coordinate(pair.Item1, pair.Item2))
            .Take(obstacleCount)
            .ToList();
        var pathFinder = new PathFinder(obstacles, 71);
        var steps = pathFinder.FindPath(new Coordinate(0, 0), new Coordinate(70, 70));
        return steps.ToString();
    }
}

