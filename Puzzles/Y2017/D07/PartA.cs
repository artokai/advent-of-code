using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D07;

[PuzzleInfo(year: 2017, day: 7, part: 1, title: "Recursive Circus")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines().SelectMany(
            line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Where(w => !w.StartsWith('(') && !w.StartsWith('-'))
                        .Select(w => w.TrimEnd(',')
                    )
            )
            .GroupBy(w => w)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderBy(g => g.Count)
            .Select(g => g.Name)
            .First();
    }
}
