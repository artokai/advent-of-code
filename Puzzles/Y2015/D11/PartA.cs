using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D11;

[PuzzleInfo(year: 2015, day: 11, part: 1, title: "Corporate Policy")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var oldPassword = Input.AsSingleLine();
        return PasswordGenerator.Generate(oldPassword);
    }
}
