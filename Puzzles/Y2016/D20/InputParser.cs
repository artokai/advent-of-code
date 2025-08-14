using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D20;

public static class InputParser
{
    public static List<Range> Parse(PuzzleInput input)
    {
        var pairs = input.AsPairs<uint, uint>(separators: new[] { '-' });
        return pairs.Select(p => new Range(p.Item1, p.Item2)).ToList();
    }
}
