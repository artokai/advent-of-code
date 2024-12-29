using System.Diagnostics.CodeAnalysis;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D05;

[PuzzleInfo(year: 2015, day: 5, part: 1, title: "Doesn't He Have Intern-Elves For This?")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var niceCount = 0;
        foreach (var line in lines)
        {
            niceCount += IsNice(line) ? 1 : 0;
        }
        return niceCount.ToString();
    }

    private bool IsNice(string line)
    {
        var prev = line[0];
        var hasDoubles = false;
        var vowels = IsVowel(prev) ? 1 : 0;
        for (var i = 1; i < line.Length; i++)
        {
            var c = line[i];
            vowels += IsVowel(c) ? 1 : 0;
            hasDoubles = hasDoubles || c == prev;
            if (prev == 'a' && c == 'b') return false;
            if (prev == 'c' && c == 'd') return false;
            if (prev == 'p' && c == 'q') return false;
            if (prev == 'x' && c == 'y') return false;
            prev = c;
        }
        return vowels >= 3 && hasDoubles;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
    }
}
