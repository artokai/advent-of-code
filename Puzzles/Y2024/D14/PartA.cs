using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D14;

[PuzzleInfo(year: 2024, day: 14, part: 1, title: "Restroom Redoubt")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var initial = InputParser.ParseInput(Input);
        var after = Simulation.MoveRobots(initial, 100);

        int qw = Simulation.MAP_WIDTH / 2;
        int qh = Simulation.MAP_HEIGHT / 2;
        var q1Cnt = CountRobots(after, 0, 0, qw, qh);
        var q2Cnt = CountRobots(after, Simulation.MAP_WIDTH - qw, 0, qw, qh);
        var q3Cnt = CountRobots(after, 0, Simulation.MAP_HEIGHT - qh, qw, qh);
        var q4Cnt = CountRobots(after, Simulation.MAP_WIDTH - qw, Simulation.MAP_HEIGHT - qh, qw, qh);

        var result = q1Cnt * q2Cnt * q3Cnt * q4Cnt;
        return result.ToString();
    }



    private long CountRobots(List<Robot> robots, int x, int y, int width, int height)
    {
        return robots
            .Where(r => r.x >= x && r.x < x + width && r.y >= y && r.y < y + height)
            .Count();
    }
}
