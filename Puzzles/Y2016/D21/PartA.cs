using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D21;

[PuzzleInfo(year: 2016, day: 21, part: 1, title: "Scrambled Letters and Hash")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var code = "abcdefgh".ToCharArray();
        var instructions = InputParser.Parse(Input);
        foreach (var instruction in instructions)
        {
            instruction.Execute(code);
        }
        return new string(code);
    }
}
