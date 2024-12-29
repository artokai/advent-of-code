using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D01;

[PuzzleInfo(year: 2015, day: 1, part: 1, title: "Not Quite Lisp")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var line = Input.AsSingleLine();
        var result = line.Aggregate(0, (acc, c) => c == '(' ? acc + 1 : acc - 1);
        return result.ToString();
    }
}