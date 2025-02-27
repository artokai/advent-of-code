using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D14;

[PuzzleInfo(year: 2015, day: 14, part: 2, title: "Reindeer Olympics")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var herd = Input.AsLines().Select(line => new Reindeer(line)).ToList();
        var scores = herd.ToDictionary(d => d.Name, _ => 0);
        for (var t = 1; t <= 2503; t++)
        {
            herd
                .Select(d => (d.Name, Distance: d.DistanceTravelledAt(t)))
                .GroupBy(x => x.Distance)
                .OrderByDescending(g => g.Key)
                .First()
                .ToList()
                .ForEach(x => scores[x.Name]++);
        }

        return scores.Values.Max().ToString();
    }
}
