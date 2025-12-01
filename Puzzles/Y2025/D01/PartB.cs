using Artokai.AOC.Core;
using Microsoft.VisualBasic;

namespace Artokai.AOC.Puzzles.Y2025.D01;

[PuzzleInfo(year: 2025, day: 1, part: 2, title: "Secret Entrance")]
public class PartB : SolverBase
{
    public const int NUMBERS = 100;
    public const int RIGHT = 1;
    public const int LEFT = -1;

    public override string Solve()
    {
        var moves = Input
            .AsLines()
            .Select(line => (line[0] == 'R' ? 1 : -1) * int.Parse(line[1..]))
            .Select(v => new
            {
                Amount = v,
                Direction = v > 0 ? RIGHT : LEFT,
                FullRotations = Math.Abs(v / NUMBERS),
                Remainder = v % NUMBERS
            })
            .ToList();

        var current = 50;
        var zeroCount = 0;
        foreach (var move in moves)
        {
            var prev = current;
            current = (current + NUMBERS + move.Remainder) % NUMBERS;

            zeroCount += move.FullRotations;
            if (prev != 0 && move.Remainder != 0)
            {
                var passedOrLandedOnZero =
                    (current == 0) ||
                    (move.Direction == RIGHT && current < prev) ||
                    (move.Direction == LEFT && current > prev);
                zeroCount += passedOrLandedOnZero ? 1 : 0;
            }
        }

        return zeroCount.ToString();
    }
}
