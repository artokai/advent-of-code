using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D02;

[PuzzleInfo(year: 2018, day: 2, part: 1, title: "Inventory Management System")]
public class PartA : SolverBase
{
    private int[] CountChars(string s)
    {
        var counts = new int['z' - 'a' + 1];
        foreach (var c in s)
        {
            counts[c - 'a']++;
        }
        return counts;
    }

    public override string Solve()
    {
        var counts = Input.AsLines().Select(CountChars).ToArray();
        var twos = counts.Count((a) => a.Contains(2));
        var threes = counts.Count((a) => a.Contains(3));
        var result = twos * threes;
        return result.ToString();
    }
}
