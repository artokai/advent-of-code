using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D02;

[PuzzleInfo(year: 2024, day: 2, part: 2, title: "Red-Nosed Reports")]
public class PartB : SolverBase
{
    private const int MAX_BAD_LEVELS = 1;

    public override string Solve()
    {
        var reports = Input.AsLists<int>();
        var result = reports.Count(r => IsSafeWithRecursion(r, 0));
        return result.ToString();
    }

    private bool IsSafeWithRecursion(List<int> report, int removalCount)
    {
        var result = Shared.IsSafe(report);
        if (result)
        {
            return true;
        }
        if (removalCount >= MAX_BAD_LEVELS)
        {
            return false;
        }

        return Enumerable
            .Range(0, report.Count)
            .Any(indexToRemove => IsSafeWithRecursion(
                report.Where((_, i) => i != indexToRemove).ToList(),
                removalCount + 1
            ));
    }
}
