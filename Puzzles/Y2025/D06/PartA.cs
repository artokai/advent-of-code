using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D06;

[PuzzleInfo(year: 2025, day: 6, part: 1, title: "Trash Compactor")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var columns = Input.AsColumns<string>();
        var sum = 0L;
        foreach (var col in columns)
        {
            var numbers = col.SkipLast(1).Select(long.Parse).ToList();
            var op = col.Last();
            var result = op switch
            {
                "+" => numbers.Sum(),
                "*" => numbers.Aggregate(1L, (acc, num) => acc * num),
                _ => throw new InvalidOperationException($"Unknown operator: {op}")
            };
            sum += result;
        }

        return sum.ToString();
    }
}
