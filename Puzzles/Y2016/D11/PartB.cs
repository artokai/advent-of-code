using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D11;

[PuzzleInfo(year: 2016, day: 11, part: 2, title: "Radioisotope Thermoelectric Generators")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (floors, bitPositions) = InputParser.ParseInput(Input);
        var elementsCount = bitPositions.Count;

        // Add the extra elements for part B
        var firstFloor = floors[0];
        var firstFloorChips = firstFloor.Microchips;
        firstFloorChips = firstFloorChips | (uint)1 << (elementsCount);
        firstFloorChips = firstFloorChips | (uint)1 << (elementsCount + 1);
        var firstFloorGenerators = firstFloor.Generators;
        firstFloorGenerators = firstFloorGenerators | (uint)1 << (elementsCount);
        firstFloorGenerators = firstFloorGenerators | (uint)1 << (elementsCount + 1);
        floors[0] = new Floor(firstFloorChips, firstFloorGenerators);
        elementsCount += 2;

        var steps = Solver.Solve(floors, elementsCount);
        if (steps > 0)
        {
            return steps.ToString();
        }
        return "No solution found.";
    }
}
