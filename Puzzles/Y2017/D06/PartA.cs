using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D06;

[PuzzleInfo(year: 2017, day: 6, part: 1, title: "Memory Reallocation")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var banks = Input
            .AsSingleLine()
            .Split('\t', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();
        var (iterations, _) = Allocator.ReAllocate(banks);
        return iterations.ToString();
    }
}
