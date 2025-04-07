using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D16;

public static class InputParser
{
    public static List<Dictionary<string, int>> ParseInput(PuzzleInput input)
    {
        return input.AsLines().Select((line) =>
        {
            var propsString = line.Split(':', 2)[1].Trim();
            var keyvaluePairs = propsString.Split(',').Select(prop =>
            {
                var parts = prop.Split(':', 2);
                return new KeyValuePair<string, int>(parts[0].Trim(), int.Parse(parts[1].Trim()));
            });
            return new Dictionary<string, int>(keyvaluePairs);
        }).ToList();
    }

    public static readonly Dictionary<string, int> TargetSue = new Dictionary<string, int> {
        {"children", 3},
        {"cats", 7},
        {"samoyeds", 2},
        {"pomeranians", 3},
        {"akitas", 0},
        {"vizslas", 0},
        {"goldfish", 5},
        {"trees", 3},
        {"cars", 2},
        {"perfumes", 1},
    };
}