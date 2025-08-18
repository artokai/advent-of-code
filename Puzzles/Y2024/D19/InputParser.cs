using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D19;

public static class InputParser
{
    public static (List<string> towels, List<string> patterns) ParseInput(PuzzleInput input)
    {
        var parts = input.SplitOnEmptyLines();
        var towels = parts[0].AsSingleLine().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        var patterns = parts[1].AsLines();
        return (towels, patterns);
    }
}
