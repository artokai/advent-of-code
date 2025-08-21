using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D16;

[PuzzleInfo(year: 2017, day: 16, part: 1, title: "Permutation Promenade")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var choreography = InputParser.Parse(Input);
        var dancers = "abcdefghijklmnop".ToCharArray();
        foreach (var move in choreography)
        {
            dancers = move(dancers);
        }
        return new string(dancers);
    }
}
