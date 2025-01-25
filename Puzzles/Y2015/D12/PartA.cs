using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D12;

[PuzzleInfo(year: 2015, day: 12, part: 1, title: "JSAbacusFramework.io")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        var sum = 0L;
        var currentNumber = "";
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (IsNumberChar(c, currentNumber))
            {
                currentNumber += c;
            }
            else
            {
                sum += (currentNumber != "" && currentNumber != "-") ? int.Parse(currentNumber) : 0;
                currentNumber = "";
            }
        }
        if (currentNumber != "" && currentNumber != "-")
        {
            sum += int.Parse(currentNumber);
        }
        return sum.ToString();
    }

    private bool IsNumberChar(char c, string currentNumber)
    {
        if (c == '-' && currentNumber == "") return true;
        if (c == '0' && (currentNumber == "" || currentNumber == "-")) return false;
        return c >= '0' && c <= '9';
    }
}
