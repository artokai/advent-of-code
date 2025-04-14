using Artokai.AOC.Core;
using Artokai.AOC.Core.Math;

namespace Artokai.AOC.Puzzles.Y2015.D20;

[PuzzleInfo(year: 2015, day: 20, part: 1, title: "Infinite Elves and Infinite Houses")]
public class PartA : SolverBase
{
    private const int PRESENT_COUNT_PER_ELF = 10;

    public override string Solve()
    {
        var maxSoFar = 0;
        var target = int.Parse(Input.AsSingleLine());
        var house = 0;
        while (maxSoFar < target)
        {
            house++;
            maxSoFar = Math.Max(maxSoFar, GetPresentCount(house));
        }
        return house.ToString();
    }

    private int GetPresentCount(int houseNumber)
    {
        var elfs = NumberTheory.GetAllFactors(houseNumber);
        return elfs
            .Aggregate(0, (acc, elf) => acc + elf * PRESENT_COUNT_PER_ELF);
    }
}
