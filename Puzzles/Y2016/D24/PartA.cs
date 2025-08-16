using System.Collections.Concurrent;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Combinatorics;

namespace Artokai.AOC.Puzzles.Y2016.D24;

[PuzzleInfo(year: 2016, day: 24, part: 1, title: "Air Duct Spelunking")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var map = new Map(Input.AsCharTable());
        var shortestPaths = map.FindAllShortestDistances();
        var ids = map.POIs.Select(p => p.Id).ToList();

        // We start from 0, so remove that before getting all permutations
        var visitOrders = ids
            .Skip(1)
            .ToList()
            .GetPermutations(PermutationMode.Linear)
            .Select(l => new List<int>([0, .. l]));

        var totals = new ConcurrentBag<int>();
        var opts = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        Parallel.ForEach(visitOrders, opts, order =>
        {
            var total = 0;
            for (var a = 0; a < order.Count - 1; a++)
            {
                var start = order[a];
                var end = order[a + 1];
                total += shortestPaths[start][end];
            }
            totals.Add(total);
        });

        var minimumDistance = totals.Min();
        return minimumDistance.ToString();
    }
}
