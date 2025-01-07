using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D10;

[PuzzleInfo(year: 2015, day: 10, part: 2, title: "Elves Look, Elves Say")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var result = Shared.Generate(input, 50);
        return result.Length.ToString();
    }
}
