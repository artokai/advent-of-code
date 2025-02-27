using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D14;

[PuzzleInfo(year: 2015, day: 14, part: 1, title: "Reindeer Olympics")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var herd = Input.AsLines().Select(line => new Reindeer(line)).ToList();
        return herd.Aggregate(
            0, 
            (best, deer) => Math.Max(best, deer.DistanceTravelledAt(2503))
        ).ToString();
    }
}
