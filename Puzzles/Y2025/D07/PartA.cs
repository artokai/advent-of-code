using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D07;

[PuzzleInfo(year: 2025, day: 7, part: 1, title: "Laboratories")]
public class PartA : SolverBase
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
        var current = Enumerable.Repeat(false, width).ToList();
        current[start] = true;

        var splitCount = 0;
        for (var y = 0; y < splitters.Count; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (current[x] && splitters[y][x])
                {
                    current[x - 1] = true;
                    current[x + 1] = true;
                    current[x] = false;
                    splitCount++;
                }
            }
        }

        return splitCount.ToString();
    }
}
