using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D11;

[PuzzleInfo(year: 2016, day: 11, part: 1, title: "Radioisotope Thermoelectric Generators")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (floors, bitPositions) = InputParser.ParseInput(Input);
        var elementsCount = bitPositions.Count;
        var steps = Solver.Solve(floors, elementsCount);
        if (steps > 0)
        {
            return steps.ToString();
        }
        return "No solution found.";
    }
}
