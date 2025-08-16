using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D01;

[PuzzleInfo(year: 2017, day: 1, part: 2, title: "Inverse Captcha")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var digits = Input.AsSingleLine().Select(c => c - '0').ToList();
        var count = digits.Count;
        var sum = 0;
        for (var i = 0; i < count; i++)
        {   
            var halfwayIndex = (i + (count / 2)) % count;
            if (digits[i] == digits[halfwayIndex])
            {
                sum += digits[i];
            }
        }
        return sum.ToString();
    } 
}
