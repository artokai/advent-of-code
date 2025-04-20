using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D07;

[PuzzleInfo(year: 2016, day: 7, part: 1, title: "Internet Protocol Version 7")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines()
            .Where(SupportsAbba)
            .Count()
            .ToString();
    }

    public bool SupportsAbba(string input)
    {
        var i = 0;
        var bracketNestingLevel = 0;
        var abbasOusideBrackets = 0;
        var abbasInBrackets = 0;
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

            var (charsProcessed, abba) = ProcessChars(input, i);
            i += charsProcessed;
            if (abba)
            {
                if (bracketNestingLevel == 0)
                {
                    abbasOusideBrackets++;
                }
                else
                {
                    abbasInBrackets++;
                }
            }
        }
        return abbasOusideBrackets > 0 && abbasInBrackets <= 0;
    }

    private (int charsProcessed, bool isAbba) ProcessChars(string input, int i)
    {
        if (i + 4 > input.Length)
        {
            return (input.Length - i, false);
        }

        var a = input[i];
        var b = input[i + 1];
        var c = input[i + 2];
        var d = input[i + 3];

        if (a == d && b == c && a != b && a >= 'a' && a <= 'z' && b >= 'a' && b <= 'z')
        {
            return (4, true);
        }

        return (1, false);
    }
}
