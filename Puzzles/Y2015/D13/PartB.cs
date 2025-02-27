using Artokai.AOC.Core;
using Artokai.AOC.Core.Combinatorics;

namespace Artokai.AOC.Puzzles.Y2015.D13;

[PuzzleInfo(year: 2015, day: 13, part: 2, title: "Knights of the Dinner Table")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var rules = Input.AsLines().Select(l => new Rule(l)).ToList();
        var persons = rules.Select(r => r.PersonA).Concat(rules.Select(r => r.PersonB)).Distinct().ToList();
        persons.Add("%_MYSELF_%");

        var best = int.MinValue;
        foreach (var permutation in persons.GetPermutations(PermutationMode.CircularIgnoreDirection))
        {
            var points = rules.Aggregate(0, (acc, rule) => acc + rule.Apply(permutation));
            best = Math.Max(best, points);
        }
        return best.ToString();
    }
}
