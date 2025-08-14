using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D21;

[PuzzleInfo(year: 2016, day: 21, part: 2, title: "Scrambled Letters and Hash")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var code = "fbgdceah".ToCharArray();
        var reversed = InputParser.Parse(Input).Reverse();
        foreach (var instruction in reversed)
        {
            instruction.Revert(code);
        }
        return new string(code);
    }
}
