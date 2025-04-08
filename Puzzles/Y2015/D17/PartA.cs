using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D17;

[PuzzleInfo(year: 2015, day: 17, part: 1, title: "No Such Thing as Too Much")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var buckets = Input.AsLines().Select(int.Parse).ToList(); ;
        var target = 150;

        var combinations = GetCombinationCount(buckets, target, 0);
        return combinations.ToString();
    }

    private int GetCombinationCount(List<int> buckets, int target, int startIndex)
    {
        if (target == 0) return 1;
        if (target < 0) return 0;

        var count = 0;
        for (int i = startIndex; i < buckets.Count; i++)
        {
            count += GetCombinationCount(buckets, target - buckets[i], i + 1);
        }

        return count;
    }
}
