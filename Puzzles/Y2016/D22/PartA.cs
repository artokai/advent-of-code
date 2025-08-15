using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D22;

[PuzzleInfo(year: 2016, day: 22, part: 1, title: "Grid Computing")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var drives = InputParser.ParseInput(Input)
            .OrderByDescending(d => d.Available)
            .ToList();

        var count = 0;
        foreach (var a in drives)
        {
            if (a.Used <= 0) { continue; }
            foreach (var b in drives)
            {
                if (b == a) { continue; }
                if (b.Available < a.Used) { break; }
                count++;
            }
        }
        return count.ToString();
    }
}
