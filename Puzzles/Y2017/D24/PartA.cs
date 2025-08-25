using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D24;

[PuzzleInfo(year: 2017, day: 24, part: 1, title: "Electromagnetic Moat")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var components = Input
            .AsPairs<int, int>(['/'])
            .ToList();

        var result = GetStrongestBridge(0, components);
        return result.ToString();
    }

    private int GetStrongestBridge(int pins, List<(int, int)> components)
    {
        var compatible = components
            .Where(c => c.Item1 == pins || c.Item2 == pins)
            .ToList();

        if (!compatible.Any())
            return 0;

        var best = int.MinValue;
        foreach (var component in compatible)
        {
            var nextPins = component.Item1 == pins ? component.Item2 : component.Item1;
            var nextComponents = components.Where(c => c != component).ToList();
            var strength = GetComponentStrength(component) + GetStrongestBridge(nextPins, nextComponents);
            best = Math.Max(best, strength);
        }
        return best;
    }

    private int GetComponentStrength((int, int) component) => component.Item1 + component.Item2;
}
