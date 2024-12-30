using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D02;

[PuzzleInfo(year: 2024, day: 2, part: 1, title: "Red-Nosed Reports")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var reports = Input.AsLists<int>();
        var result = reports.Count(Shared.IsSafe);
        return result.ToString();
    }
}
