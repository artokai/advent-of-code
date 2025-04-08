using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D17;

[PuzzleInfo(year: 2015, day: 17, part: 2, title: "No Such Thing as Too Much")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var buckets = Input.AsLines().Select(int.Parse).ToList();
        var target = 150;

        var minContainerCnt = GetMinContainerCount(buckets, target, 0, 0);
        var combinations = GetCombinationCount(buckets, target, 0, minContainerCnt);
        return combinations.ToString();
    }

    private int GetMinContainerCount(List<int> buckets, int target, int startIndex, int current)
    {
        if (target == 0) return current;
        if (target < 0) return int.MaxValue;

        var best = int.MaxValue;
        for (int i = startIndex; i < buckets.Count; i++)
        {
            var cnt = GetMinContainerCount(buckets, target - buckets[i], i + 1, current + 1);
            best = Math.Min(best, cnt);
        }
        return best;
    }

    private int GetCombinationCount(List<int> buckets, int target, int startIndex, int containerCnt)
    {
        if (target == 0) return 1;
        if (target < 0) return 0;
        if (containerCnt <= 0) return 0;

        var count = 0;
        for (int i = startIndex; i < buckets.Count; i++)
        {
            count += GetCombinationCount(buckets, target - buckets[i], i + 1, containerCnt - 1);
        }
        return count;
    }
}
