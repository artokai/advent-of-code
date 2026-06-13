using System.Runtime.InteropServices;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D03;

[PuzzleInfo(year: 2018, day: 3, part: 2, title: "No Matter How You Slice It")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var claims = InputParser.Parse(Input);
        var overlapping = new HashSet<Claim>();
        for (var i = 0; i < claims.Count - 2; i++)
        {
            var a = claims[i];
            for (var j = i + 1; j < claims.Count - 1; j++)
            {
                var b = claims[j];
                if (Overlaps(a, b))
                {
                    overlapping.Add(a);
                    overlapping.Add(b);
                }
            }
        }

        var result = claims.First(c => !overlapping.Contains(c));
        return result.Id.ToString();
    }

    private bool Overlaps(Claim a, Claim b)
    {
        return a.X < b.X + b.Width &&
               a.X + a.Width > b.X &&
               a.Y < b.Y + b.Height &&
               a.Y + a.Height > b.Y;
    }
}
