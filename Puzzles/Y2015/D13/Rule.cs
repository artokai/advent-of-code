using Artokai.AOC.Core;
using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2015.D13;

public class Rule
{
    public string PersonA { get; set; }
    public string PersonB { get; set; }
    public int Gain { get; set; }

    public Rule(string line)
    {
        var regex = new Regex(@"(?<PersonA>\w+) would (?<Action>gain|lose) (?<Gain>\d+) happiness units by sitting next to (?<PersonB>\w+)\.");
        var match = regex.Match(line);
        if (match.Success)
        {
            PersonA = match.Groups["PersonA"].Value;
            PersonB = match.Groups["PersonB"].Value;
            Gain = int.Parse(match.Groups["Gain"].Value) * (match.Groups["Action"].Value == "gain" ? 1 : -1);
        }
        else
        {
            throw new ArgumentException("Invalid line format", nameof(line));
        }
    }

    public int Apply(IList<string> persons)
    {
        var indexA = persons.IndexOf(PersonA);
        if (indexA < 0) return 0;

        var indexB = persons.IndexOf(PersonB);
        if (indexB < 0) return 0;

        if (Math.Abs(indexA - indexB) == 1 || indexA == 0 && indexB == persons.Count - 1 || indexA == persons.Count - 1 && indexB == 0)
        {
            return Gain;
        }
        return 0;
    }
}
