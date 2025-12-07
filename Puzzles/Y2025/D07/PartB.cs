using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D07;

[PuzzleInfo(year: 2025, day: 7, part: 2, title: "Laboratories")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var start = lines[0].IndexOf('S') + 1;
        var splitters = lines.Skip(1)
            .Select(line => $".{line}.".Select(c => c != '.').ToArray())
            .Where(arr => arr.Any(b => b))
            .ToList();

        var width = splitters[0].Length;
        var current = Enumerable.Repeat(0L, width).ToList();
        current[start] = 1L;

        for (var y = 0; y < splitters.Count; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (current[x] > 0 && splitters[y][x])
                {
                    current[x - 1] += current[x];
                    current[x + 1] += current[x];
                    current[x] = 0;
                }
            }
        }

        return current.Sum().ToString();
    }
}
