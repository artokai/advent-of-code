using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D11;

[PuzzleInfo(year: 2024, day: 11, part: 1, title: "Plutonian Pebbles")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var stones = Input.AsSingleLine().Split(' ').Select(long.Parse);
        var result = Shared.Simulate(stones, 25);
        return result.ToString();
    }
}
