using Artokai.AOC.Core;
using Artokai.AOC.Core.Math;

namespace Artokai.AOC.Puzzles.Y2015.D20;

[PuzzleInfo(year: 2015, day: 20, part: 2, title: "Infinite Elves and Infinite Houses")]
public class PartB : SolverBase
{
    private const int PRESENT_COUNT_PER_ELF = 11;
    private const int MAX_HOUSES_PER_ELF = 50;

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
            .Where(n => houseNumber / n <= MAX_HOUSES_PER_ELF)
            .Aggregate(0, (acc, elf) => acc + elf * PRESENT_COUNT_PER_ELF);
    }
}
