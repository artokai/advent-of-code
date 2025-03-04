using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D15;

public record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);

public class InputParser
{
    public static List<Ingredient> Parse(PuzzleInput input)
    {
        var re = new Regex(@"^(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavor>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)$");
        return input.AsLines().Select(line =>
        {
            var match = re.Match(line);
            if (!match.Success) throw new FormatException($"Invalid input: {line}");
            return new Ingredient(
                match.Groups["name"].Value,
                int.Parse(match.Groups["capacity"].Value),
                int.Parse(match.Groups["durability"].Value),
                int.Parse(match.Groups["flavor"].Value),
                int.Parse(match.Groups["texture"].Value),
                int.Parse(match.Groups["calories"].Value)
            );
        }).ToList();
    }
}
