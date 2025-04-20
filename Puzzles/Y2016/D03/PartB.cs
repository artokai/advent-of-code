using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D03;

[PuzzleInfo(year: 2016, day: 3, part: 2, title: "Squares With Three Sides")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var values = Input.AsColumns<int>().SelectMany(c => c).ToList();
        var count = 0;
        for (int i = 0; i < values.Count(); i += 3)
        {
            var a = values[i];
            var b = values[i + 1];
            var c = values[i + 2];
            if (a + b > c && a + c > b && b + c > a)
            {
                count++;
            }
        }
        return count.ToString();
    }
}
