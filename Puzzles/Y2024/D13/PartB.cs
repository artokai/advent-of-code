using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D13;

[PuzzleInfo(year: 2024, day: 13, part: 2, title: "Claw Contraption")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var machines = InputParser.Parse(Input);
        var price = machines
            .Select(m => new Machine
            {
                deltaXforA = m.deltaXforA,
                deltaYforA = m.deltaYforA,
                deltaXforB = m.deltaXforB,
                deltaYforB = m.deltaYforB,
                targetX = 10000000000000 + m.targetX,
                targetY = 10000000000000 + m.targetY
            })
            .Aggregate(0L, (acc, m) => acc + m.GetPrice());
        return price.ToString();
    }
}
