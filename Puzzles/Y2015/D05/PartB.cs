using System.Runtime.InteropServices;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D05;

[PuzzleInfo(year: 2015, day: 5, part: 2, title: "Doesn't He Have Intern-Elves For This?")]
public class PartB : SolverBase
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
        if (!HasPairs(line)) return false;
        if (!HasRepeatingLetter(line)) return false;
        return true;
    }

    private bool HasPairs(string line)
    {
        for (var i = 0; i < line.Length - 3; i++)
        {
            var pair = line.Substring(i, 2);
            for (var j = i + 2; j < line.Length - 1; j++)
            {
                var secondPair = line.Substring(j, 2);
                if (pair == secondPair)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool HasRepeatingLetter(string line)
    {
        for (var i = 2; i < line.Length; i++)
        {
            if (line[i] == line[i - 2]) return true;
        }
        return false;
    }
}
