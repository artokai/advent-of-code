using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D07;

[PuzzleInfo(year: 2015, day: 7, part: 1, title: "Some Assembly Required")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var circuit = new Circuit(Input);
        circuit.Simulate();
        return circuit.Lines["a"].ToString();
    }
}
