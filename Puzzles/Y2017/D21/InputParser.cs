using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2017.D21;

public static class InputParser
{
    public static Dictionary<int, Dictionary<string, Grid>> Parse(PuzzleInput input)
    {
        var lines = input.AsLines();
        var patterns = new Dictionary<int, Dictionary<string, Grid>>();
        patterns[2] = new Dictionary<string, Grid>();
        patterns[3] = new Dictionary<string, Grid>();

        foreach (var line in lines)
        {
            var parts = line.Split(" => ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var from = Grid.Parse(parts[0]);
            var to = Grid.Parse(parts[1]);
            var size = from.Size;

            foreach (var pattern in ProducePermutations(from))
            {
                patterns[size][pattern.ToString()] = to;
            }
        }
        return patterns;
    }

    public static IEnumerable<Grid> ProducePermutations(Grid current)
    {
        yield return current;
        yield return current.FlipHorizontal();
        for (var i = 0; i < 3; i++)
        {
            current = current.RotateLeft();
            yield return current;
            yield return current.FlipHorizontal();
        }
    }
}
