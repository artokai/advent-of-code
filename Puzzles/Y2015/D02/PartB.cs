using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D02;

[PuzzleInfo(year: 2015, day: 2, part: 2, title: "I Was Told There Would Be No Math")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var total = 0L;
        var lines = Input.AsLines();
        foreach (var line in lines)
        {
            // l * w * h
            var dimensions = line.Split('x').Select(int.Parse).ToArray();
            var perimLW = 2 * dimensions[0] + 2 * dimensions[1];
            var perimLH = 2 * dimensions[0] + 2 * dimensions[2];
            var perimWH = 2 * dimensions[1] + 2 * dimensions[2];
            var smallestPerim = Math.Min(perimLW, Math.Min(perimLH, perimWH));
            var volume = dimensions[0] * dimensions[1] * dimensions[2];
            total += smallestPerim + volume;
        }
        return total.ToString();
    }
}
