using System.Text;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D06;

[PuzzleInfo(year: 2025, day: 6, part: 2, title: "Trash Compactor")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var pivoted = Pivot(Input.AsLines());

        var sum = 0L;
        var numbers = new List<long>();
        foreach (var line in pivoted)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var op = line[^1] switch
            {
                '+' or '*' => line[^1].ToString(),
                _ => ""
            };

            var numberStr = op != "" ? line[..^1].Trim() : line.Trim();
            numbers.Add(long.Parse(numberStr));

            if (op != "")
            {
                var result = op switch
                {
                    "+" => numbers.Sum(),
                    "*" => numbers.Aggregate(1L, (acc, num) => acc * num),
                    _ => throw new InvalidOperationException($"Unknown operator: {op}")
                };
                sum += result;
                numbers.Clear();
            }
        }

        return sum.ToString();
    }

    private List<string> Pivot(List<string> lines)
    {
        var sbs = new List<StringBuilder>();

        var width = lines.First().Length;
        while (sbs.Count < width)
            sbs.Add(new StringBuilder());

        foreach (var line in lines)
        {
            for (var charIdx = width - 1; charIdx >= 0; charIdx--)
            {
                var sbsIdx = width - charIdx - 1;
                var c = line[charIdx];
                sbs[sbsIdx].Append(c);
            }
        }

        return sbs.Select(sb => sb.ToString()).ToList();
    }
}
