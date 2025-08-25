using Artokai.AOC.Core;
using Microsoft.VisualBasic;

namespace Artokai.AOC.Puzzles.Y2017.D24;

[PuzzleInfo(year: 2017, day: 24, part: 2, title: "Electromagnetic Moat")]
public class PartB : SolverBase
{
    Dictionary<int, int> strongestLengths = new();

    public override string Solve()
    {
        var components = Input
            .AsPairs<int, int>(['/'])
            .ToList();

        var (result, _) = GetStrongestBridge(0, 0, components);
        return result.ToString();
    }

    private (int, int) GetStrongestBridge(int pins, int currentLength, List<(int, int)> components)
    {
        var compatible = components
            .Where(c => c.Item1 == pins || c.Item2 == pins)
            .ToList();

        if (!compatible.Any())
            return (0, currentLength);

        var longest = int.MinValue;
        var best = int.MinValue;
        foreach (var component in compatible)
        {
            var nextPins = component.Item1 == pins ? component.Item2 : component.Item1;
            var nextComponents = components.Where(c => c != component).ToList();
            var (strength, length) = GetStrongestBridge(nextPins, currentLength + 1, nextComponents);

            if (length > longest)
            {
                longest = length;
                best = int.MinValue;
            }

            if (length == longest)
            {
                best = Math.Max(best, GetComponentStrength(component) + strength);
            }
        }
        return (best, longest);
    }

    private int GetComponentStrength((int, int) component) => component.Item1 + component.Item2;
}
