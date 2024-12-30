using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D01;

[PuzzleInfo(year: 2024, day: 1, part: 2, title: "Historian Hysteria")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var columns = Input.AsColumns<int>();
        var counts = columns[1].CountBy(b => b).ToDictionary();
        var result = columns[0].Select(a => a * counts.GetValueOrDefault(a, 0)).Sum();
        return result.ToString();
    }
}
