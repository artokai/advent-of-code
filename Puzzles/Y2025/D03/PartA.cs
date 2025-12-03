using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D03;

[PuzzleInfo(year: 2025, day: 3, part: 1, title: "Lobby")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var banks = Input
            .AsLines()
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToList())
            .ToList();

        var sum = banks
            .Select(GetMaxPair)
            .Select(tuple => tuple.Item1 * 10 + tuple.Item2)
            .Sum();

        return sum.ToString();
    }

    private Tuple<int, int> GetMaxPair(List<int> bank)
    {
        var first = bank.Take(bank.Count - 1).Max();
        var second = bank.Skip(bank.IndexOf(first) + 1).Max();
        return Tuple.Create(first, second);
    }
}
