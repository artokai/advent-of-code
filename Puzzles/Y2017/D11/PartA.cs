using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D11;

[PuzzleInfo(year: 2017, day: 11, part: 1, title: "Hex Ed")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Input.AsSingleLine().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(QrsCoord.Parse)
            .Aggregate(new QrsCoord(0, 0, 0), (current, next) => current + next)
            .Length
            .ToString();
    }
}
