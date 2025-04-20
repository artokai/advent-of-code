using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D07;

[PuzzleInfo(year: 2016, day: 7, part: 2, title: "Internet Protocol Version 7")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines()
            .Where(SupportsSSL)
            .Count()
            .ToString();
    }

    public bool SupportsSSL(string input)
    {
        var i = 0;
        var bracketNestingLevel = 0;
        var abas = new HashSet<string>();
        var babs = new HashSet<string>();

        while (i < input.Length)
        {
            if (input[i] == '[')
            {
                bracketNestingLevel++;
                i++;
                continue;
            }

            if (input[i] == ']')
            {
                bracketNestingLevel = Math.Max(0, bracketNestingLevel - 1);
                i++;
                continue;
            }

            var (charsProcessed, abaOrBab) = ProcessChars(input, i);
            i += charsProcessed;
            if (abaOrBab != null)
            {
                if (bracketNestingLevel == 0)
                {
                    abas.Add(abaOrBab);
                }
                else
                {
                    babs.Add(new string([abaOrBab[1], abaOrBab[0], abaOrBab[1]]));
                }
            }
        }

        if (abas.Count == 0)
        {
            return false;
        }

        return abas.Any(aba => babs.Any(bab => bab == aba));
    }

    private (int charsProcessed, string? abaOrBab) ProcessChars(string input, int i)
    {
        if (i + 3 > input.Length)
        {
            return (input.Length - i, null);
        }

        var a = input[i];
        var b = input[i + 1];
        var c = input[i + 2];

        // aba or bab
        // - return only one processed char since overlapping sequences are allowed
        if (a == c && a != b && a >= 'a' && a <= 'z' && b >= 'a' && b <= 'z')
        {
            return (1, new string([a, b, a]));
        }

        return (1, null);
    }
}
