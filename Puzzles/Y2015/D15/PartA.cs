using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D15;

[PuzzleInfo(year: 2015, day: 15, part: 1, title: "Science for Hungry People")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var ingredients = InputParser.Parse(Input);
        var cookieFactory = new CookieFactory(ingredients);
        var mixtures = cookieFactory.GetMixtures(ingredients.Count, 100);
        var score = mixtures.Aggregate(
            0,
            (best, m) => Math.Max(best, cookieFactory.CalculateScore(m))
        );
        return score.ToString();
    }
}
