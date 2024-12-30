using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D05;

[PuzzleInfo(year: 2024, day: 5, part: 2, title: "Print Queue")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var inputs = Input.SplitOnEmptyLines();
        var sortRules = inputs[0].AsPairs<int, int>(separators: ['|']);
        var comparer = Shared.CreateComparer(sortRules);
        var batches = inputs[1].AsLists<int>(separators: [',']);

        var result = 0;
        var goodCnt = 0;
        var badCnt = 0;
        foreach (var batch in batches)
        {
            var sorted = batch.Order(comparer).ToList();
            if (string.Join(',', batch) == string.Join(',', sorted))
            {
                goodCnt++;
            }
            else
            {
                badCnt++;
                result += sorted[sorted.Count / 2];
            }
        }

        return result.ToString();
    }
}
