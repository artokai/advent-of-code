using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D07;

[PuzzleInfo(year: 2024, day: 7, part: 1, title: "Bridge Repair")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var rows = InputParser.ParseInput(lines);
        long sumOfRowResults = 0;
        foreach (var (result, parts) in rows)
        {
            if (SolveRow(result, parts)) { sumOfRowResults += result; }
        }
        return sumOfRowResults.ToString();
    }

    private bool SolveRow(long result, List<long> parts)
    {
        if (parts.Count == 1)
        {
            return parts[0] == result;
        }

        var sum = parts[0] + parts[1];
        var sumParts = new List<long> { sum };
        if (parts.Count > 2)
        {
            sumParts.AddRange(parts[2..]);
        }
        if (SolveRow(result, sumParts)) { return true; }

        var product = parts[0] * parts[1];
        var productParts = new List<long> { product };
        if (parts.Count > 2)
        {
            productParts.AddRange(parts[2..]);
        }
        if (SolveRow(result, productParts)) { return true; }

        return false;
    }
}
