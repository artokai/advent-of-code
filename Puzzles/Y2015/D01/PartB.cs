using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D01;

[PuzzleInfo(year: 2015, day: 1, part: 2, title: "Not Quite Lisp")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var line = Input.AsSingleLine();
        var floor = 0;
        var position = 0;
        while (floor >= 0)
        {
            var c = line[position++];
            floor += c == '(' ? 1 : -1;
        }
        return position.ToString();
    }
}