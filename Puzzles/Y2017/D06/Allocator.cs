
namespace Artokai.AOC.Puzzles.Y2017.D06;

public class Allocator
{

    public static (int iterations, int cycleLength) ReAllocate(List<int> banks)
    {
        var iteration = 0;
        var seenIteration = new Dictionary<int, int>();
        var hash = GetHash(banks);
        while (!seenIteration.ContainsKey(hash))
        {
            seenIteration.Add(hash, iteration);
            Distribute(banks);
            hash = GetHash(banks);
            iteration++;
        }

        var firstSeen = seenIteration[hash];
        var cycleLength = iteration - firstSeen;
        return (iteration, cycleLength);
    }

    private static void Distribute(List<int> banks)
    {
        var maxIndex = banks.IndexOf(banks.Max());
        var blocks = banks[maxIndex];
        banks[maxIndex] = 0;

        for (var i = 1; i <= blocks; i++)
        {
            var index = (maxIndex + i) % banks.Count;
            banks[index]++;
        }
    }

    private static int GetHash(List<int> banks) => banks.Aggregate(new HashCode(), (acc, cur) => { acc.Add(cur); return acc; }).ToHashCode();

}