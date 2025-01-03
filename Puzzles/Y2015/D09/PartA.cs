using System.Text.RegularExpressions;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D09;

[PuzzleInfo(year: 2015, day: 9, part: 1, title: "All in a Single Night")]
public class PartA : SolverBase
{
    private Dictionary<string, List<(string Target, int Distance)>> _edges = new();

    public override string Solve()
    {
        _edges = InputParser.ParseInput(Input);
        var allCities = _edges.Keys.ToHashSet();
        allCities.UnionWith(_edges.Values.SelectMany(x => x.Select(e => e.Target)));

        var minDistance = int.MaxValue;
        foreach (var city in allCities)
        {
            var distance = CalculateMinDistance(city, 0, allCities.Where(c => c != city));
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        return minDistance.ToString();
    }

    private int CalculateMinDistance(string start, int distanceSoFar, IEnumerable<string> remaining)
    {
        if (!remaining.Any())
        {
            return distanceSoFar;
        }

        var minDistance = int.MaxValue;
        foreach (var target in remaining)
        {
            var edgesFromStart = _edges.GetValueOrDefault(start, new List<(string Target, int Distance)>());
            var edgeToCity = edgesFromStart.FirstOrDefault(e => e.Target == target);
            if (edgeToCity == default)
            {
                continue;
            }

            var distance = CalculateMinDistance(target, distanceSoFar + edgeToCity.Distance, remaining.Where(c => c != target));
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        return minDistance;
    }
}
