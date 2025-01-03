using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D09;

[PuzzleInfo(year: 2015, day: 9, part: 2, title: "All in a Single Night")]
public class PartB : SolverBase
{
    private Dictionary<string, List<(string Target, int Distance)>> _edges = new();

    public override string Solve()
    {
        _edges = InputParser.ParseInput(Input);
        var allCities = _edges.Keys.ToHashSet();
        allCities.UnionWith(_edges.Values.SelectMany(x => x.Select(e => e.Target)));

        var maxDistance = int.MinValue;
        foreach (var city in allCities)
        {
            var distance = CalculateMaxDistance(city, 0, allCities.Where(c => c != city));
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }
        return maxDistance.ToString();
    }

    private int CalculateMaxDistance(string start, int distanceSoFar, IEnumerable<string> remaining)
    {
        if (!remaining.Any())
        {
            return distanceSoFar;
        }

        var maxDistance = int.MinValue;
        foreach (var target in remaining)
        {
            var edgesFromStart = _edges.GetValueOrDefault(start, new List<(string Target, int Distance)>());
            var edgeToCity = edgesFromStart.FirstOrDefault(e => e.Target == target);
            if (edgeToCity == default)
            {
                continue;
            }

            var distance = CalculateMaxDistance(target, distanceSoFar + edgeToCity.Distance, remaining.Where(c => c != target));
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }
        return maxDistance;
    }
}
