using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D19;

[PuzzleInfo(year: 2015, day: 19, part: 1, title: "Medicine for Rudolph")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (replacements, molecule) = InputParser.Parse(Input);
        var replaced = Replace(molecule, replacements);
        return replaced.Count().ToString();
    }

    public static IEnumerable<string> Replace(string molecule, Dictionary<string, List<string>> replacements)
    {
        var result = new HashSet<string>();
        foreach (var replacement in replacements)
        {
            var searchStr = replacement.Key;
            var index = molecule.IndexOf(searchStr);
            while (index != -1)
            {
                var newMolecule = molecule.Remove(index, searchStr.Length);
                foreach (var replacementValue in replacement.Value)
                {
                    var nextMolecule = newMolecule.Insert(index, replacementValue);
                    result.Add(nextMolecule);
                }
                index = molecule.IndexOf(searchStr, index + 1);
            }
        }
        return result;
    }
}
