using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D19;

[PuzzleInfo(year: 2015, day: 19, part: 2, title: "Medicine for Rudolph")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (replacements, target) = InputParser.Parse(Input);

        // Reverse the replacements to work backward from the target molecule to "e".
        var reversed = new List<(string key, string replacement)>();
        foreach (var kvp in replacements)
        {
            foreach (var replacement in kvp.Value)
            {
                reversed.Add((replacement, kvp.Key));
            }
        }

        // Breadth-first search (BFS) or depth-first search (DFS) could be used here,
        // but it would quickly become inefficient for larger molecules.
        //
        // Using a greedy algorithm, we prioritize replacements with the fewest matches
        // in the current molecule, reducing its complexity step by step.
        //
        // This approach work for this specific input, but may not work for all cases.
        var count = 0;
        var current = target;
        while (current != "e")
        {
            var replacement = reversed.Select(pair =>
            {
                var regex = new Regex(pair.key);
                var matches = regex.Matches(current);
                return (Key: pair.key, Replacement: pair.replacement, matches.Count);
            })
            .Where(pair => pair.Count > 0)
            .OrderBy(pair => pair.Count)
            .FirstOrDefault();

            if (replacement == default)
            {
                return "Not found!";
            }

            current = current.Replace(replacement.Key, replacement.Replacement);
            count += replacement.Count;
        }
        return count.ToString();
    }
}
