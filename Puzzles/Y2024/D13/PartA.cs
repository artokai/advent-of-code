using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D13;

[PuzzleInfo(year: 2024, day: 13, part: 1, title: "Claw Contraption")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var machines = InputParser.Parse(Input);
        var price = machines.Aggregate(0L, (acc, m) => acc + m.GetPrice());
        return price.ToString();
    }
}
