using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D05;

[PuzzleInfo(year: 2018, day: 5, part: 2, title: "Alchemical Reduction")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var uniqueChars = new HashSet<char>(input.ToLower());
        var minLength = input.Length;
        foreach (var c in uniqueChars)
        {
            var trimmed = input.Replace(c.ToString(), "", StringComparison.OrdinalIgnoreCase);
            var reduced = Reducer.Reduce(trimmed);
            minLength = Math.Min(minLength, reduced.Length);
        }
        return minLength.ToString();
    }
}
