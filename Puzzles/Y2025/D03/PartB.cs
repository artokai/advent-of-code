using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D03;

[PuzzleInfo(year: 2025, day: 3, part: 2, title: "Lobby")]
public class PartB : SolverBase
{
    private const int DIGITS = 12;

    public override string Solve()
    {
        var banks = Input
            .AsLines()
            .Select(line => line.Select(c => long.Parse(c.ToString())).ToList())
            .ToList();

        var sum = banks
            .Select(b => SelectMaxDigitOfLength(b, DIGITS))
            .Sum();

        return sum.ToString();
    }

    private long SelectMaxDigitOfLength(List<long> bank, int length)
    {
        var selectedBatteryVoltage = bank.Take(bank.Count - (length - 1)).Max();
        if (length == 1)
        {
            return selectedBatteryVoltage;
        }

        var index = bank.IndexOf(selectedBatteryVoltage);
        var remaining = bank.Skip(index + 1).ToList();
        var shiftedValue = selectedBatteryVoltage * (long)Math.Pow(10, length - 1);

        return shiftedValue + SelectMaxDigitOfLength(remaining, length - 1);
    }
}
