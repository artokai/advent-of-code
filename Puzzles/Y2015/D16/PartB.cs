using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D16;

[PuzzleInfo(year: 2015, day: 16, part: 2, title: "Aunt Sue")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var aunts = InputParser.ParseInput(Input);
        var target = InputParser.TargetSue;
        var best = aunts.Select((aunt, index) =>
        {
            return new
            {
                SueId = index + 1,
                Score = CalculateScore(aunt, target),
            };
        })
        .MaxBy(x => x.Score);

        return (best?.SueId ?? -1).ToString();
    }

    public int CalculateScore(Dictionary<string, int> aunt, Dictionary<string, int> target)
    {
        var score = 0;
        foreach (var key in aunt.Keys.Where(target.ContainsKey))
        {
            score += key switch
            {
                "cats" or "trees" => (aunt[key] > target[key]) ? 1 : -100,
                "pomeranians" or "goldfish" => (aunt[key] < target[key]) ? 1 : -100,
                _ => (aunt[key] == target[key]) ? 1 : -100,
            };
        }
        return score;
    }
}
