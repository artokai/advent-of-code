using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D04;

[PuzzleInfo(year: 2017, day: 4, part: 1, title: "High-Entropy Passphrases")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var passphrases = Input.AsLists<string>();
        var validCount = passphrases
            .Where(phrase => phrase.Distinct().Count() == phrase.Count())
            .Count();
        return validCount.ToString();
    }
}
