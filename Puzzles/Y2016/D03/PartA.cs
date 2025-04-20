using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D03;

[PuzzleInfo(year: 2016, day: 3, part: 1, title: "Squares With Three Sides")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines()
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray())
            .Where(t => t[0] + t[1] > t[2])
            .Where(t => t[0] + t[2] > t[1])
            .Where(t => t[1] + t[2] > t[0])
            .Count()
            .ToString();
    }
}
