using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D11;

[PuzzleInfo(year: 2015, day: 11, part: 2, title: "Corporate Policy")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var oldPassword = Input.AsSingleLine();
        var nextPassword = PasswordGenerator.Generate(oldPassword);
        return PasswordGenerator.Generate(nextPassword);
    }
}
