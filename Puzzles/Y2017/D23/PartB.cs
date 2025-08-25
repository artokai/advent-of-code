using Artokai.AOC.Core;
using Artokai.AOC.Core.Math;

namespace Artokai.AOC.Puzzles.Y2017.D23;

[PuzzleInfo(year: 2017, day: 23, part: 2, title: "Coprocessor Conflagration")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var nonPrimes = 0;
        for (var n = 105_700; n <= 122_700; n += 17)
        {
            if (!NumberTheory.IsPrime(n))
            {
                nonPrimes++;
            }
        }
        return nonPrimes.ToString();
    }
}
