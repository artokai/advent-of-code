using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D19;

[PuzzleInfo(year: 2024, day: 19, part: 1, title: "Linen Layout")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (towels, patterns) = InputParser.ParseInput(Input);
        return patterns.Count(p => IsPossible(p, towels)).ToString();
    }

    public bool IsPossible(string pattern, List<string> towels)
    {
        var stack = new Stack<int>();
        stack.Push(0);
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            var tailPattern = pattern.Substring(current);
            foreach (var t in towels) {
                if (tailPattern.StartsWith(t))
                {
                    if (current + t.Length == pattern.Length)
                    {
                        return true;
                    }
                    stack.Push(current + t.Length);
                }
            }
        }
        return false;
    }
}
