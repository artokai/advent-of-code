using System.Collections.Concurrent;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Combinatorics;

namespace Artokai.AOC.Puzzles.Y2016.D24;

[PuzzleInfo(year: 2016, day: 24, part: 2, title: "Air Duct Spelunking")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var map = new Map(Input.AsCharTable());
        var shortestPaths = map.FindAllShortestDistances();
        var ids = map.POIs.Select(p => p.Id).ToList();

        var visitOrders = ids
            .GetPermutations(PermutationMode.CircularIgnoreDirection)
            .ToList();

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

            // Return to start
            total += shortestPaths[order[^1]][order[0]];
            totals.Add(total);
        });

        var minimumDistance = totals.Min();
        return minimumDistance.ToString();
    }
}
