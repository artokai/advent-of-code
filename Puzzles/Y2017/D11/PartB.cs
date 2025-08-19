using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D11;

[PuzzleInfo(year: 2017, day: 11, part: 2, title: "Hex Ed")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var maxDistance = int.MinValue;
        var current = new QrsCoord(0, 0, 0);
        var steps = Input.AsSingleLine()
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(QrsCoord.Parse);

        foreach (var step in steps)
        {
            current += step;
            maxDistance = Math.Max(maxDistance, current.Length);
        }
        return maxDistance.ToString();
    }
}
