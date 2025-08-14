using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D19;

[PuzzleInfo(year: 2016, day: 19, part: 1, title: "An Elephant Named Joseph")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var round = 1;
        var count = int.Parse(Input.AsSingleLine());
        var elfs = new List<int>(Enumerable.Range(1, count));
        while (elfs.Count > 1)
        {
            var isOdd = elfs.Count % 2 != 0;
            elfs = elfs.Where((_, index) => index % 2 == 0).ToList();
            if (elfs.Count > 1 && isOdd)
            {
                elfs = elfs.Skip(1).ToList();
            }
            round++;
        }
        return elfs.First().ToString();
    }
}
