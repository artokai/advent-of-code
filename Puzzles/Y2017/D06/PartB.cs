using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D06;

[PuzzleInfo(year: 2017, day: 6, part: 2, title: "Memory Reallocation")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var banks = Input
            .AsSingleLine()
            .Split('\t', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();
        var (_, cycleLength) = Allocator.ReAllocate(banks);
        return cycleLength.ToString();
    }
}
