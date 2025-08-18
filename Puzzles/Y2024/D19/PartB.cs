using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D19;

[PuzzleInfo(year: 2024, day: 19, part: 2, title: "Linen Layout")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (towels, patterns) = InputParser.ParseInput(Input);
        var memo = new Dictionary<string, long>();
        var solutions = 0L;
        foreach (var pattern in patterns)
        {
            var cnt = CountSolutions(pattern, towels, memo);
            solutions += cnt;
        };
        return solutions.ToString();

    }
    public long CountSolutions(string pattern, List<string> towels, Dictionary<string, long> memo)
    {
        if (pattern == "")
        {
            return 1L;
        }

        if (memo.ContainsKey(pattern))
        {
            return memo[pattern];
        }

        var count = 0L;
        foreach (var t in towels)
        {
            if (pattern.StartsWith(t))
            {
                var right = pattern.Substring(t.Length);
                count += CountSolutions(right, towels, memo);
            }
        }

        memo[pattern] = count;
        return count;
    }
}
