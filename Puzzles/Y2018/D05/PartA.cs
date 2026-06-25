using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D05;

[PuzzleInfo(year: 2018, day: 5, part: 1, title: "Alchemical Reduction")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var output = Reducer.Reduce(input);
        return output.Length.ToString();
    }
}
