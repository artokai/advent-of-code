using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D19;

public static class InputParser
{
    public static (
        Dictionary<string, List<string>> Replacements,
        string InputMolecule
    ) Parse(PuzzleInput input)
    {
        var inputParts = input.SplitOnEmptyLines();
        if (inputParts == null || inputParts.Count != 2)
            throw new ArgumentException("Input is not in the expected format.");
        var replacements = inputParts[0].AsLines()
            .Select(line => line.Split(" => "))
            .Select(pair => (
                Element: pair[0].Trim(),
                Replacement: pair[1].Trim()
            ))
            .GroupBy(pair => pair.Element, tuple => tuple.Replacement)
            .ToDictionary(
                g => g.Key,
                g => g.ToList()
            );
        return (replacements, inputParts[1].AsSingleLine());
    }
}
