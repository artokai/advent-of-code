using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D09;

[PuzzleInfo(year: 2018, day: 9, part: 2, title: "Marble Mania")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var parts = Input.AsSingleLine().Split(' ');
        var playerCount = int.Parse(parts[0]);
        var maxMarble = 100 * int.Parse(parts[6]);

        var result = Solver.Solve(playerCount, maxMarble);
        return result.ToString();
    }
}
