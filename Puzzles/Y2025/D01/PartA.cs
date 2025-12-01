using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D01;

[PuzzleInfo(year: 2025, day: 1, part: 1, title: "Secret Entrance")]
public class PartA : SolverBase
{
    public const int NUMBERS = 100;

    public override string Solve()
    {
        var deltas = Input
            .AsLines()
            .Select(line => (line[0] == 'R' ? 1 : -1) * int.Parse(line[1..]))
            .Select(v => v % NUMBERS)
            .ToList();

        var current = 50;
        var zeroCount = 0;
        foreach (var delta in deltas)
        {
            current = (current + NUMBERS + delta) % NUMBERS;
            zeroCount += current == 0 ? 1 : 0;
        }

        return zeroCount.ToString();
    }
}
