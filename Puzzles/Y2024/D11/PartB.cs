using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D11;

[PuzzleInfo(year: 2024, day: 11, part: 2, title:"Plutonian Pebbles")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var stones = Input.AsSingleLine().Split(' ').Select(long.Parse);
        var result = Shared.Simulate(stones, 75);
        return result.ToString();
    }
}
