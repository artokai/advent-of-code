using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2025.D09;

[PuzzleInfo(year: 2025, day: 9, part: 1, title: "Movie Theater")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var tiles = Input.AsLines()
            .Select(line =>
            {
                var parts = line.Split(',');
                return new Vector2DInt(int.Parse(parts[0]), int.Parse(parts[1]));
            })
            .ToList();

        var maxSize = long.MinValue;
        foreach (var pair in GetUniquePairs(tiles))
        {
            var size = GetSize(pair.A, pair.B);
            if (size > maxSize)
            {
                maxSize = size;
            }
        }

        return maxSize.ToString();
    }

    private long GetSize(Vector2DInt a, Vector2DInt b) =>
        (Math.Abs(a.X - b.X) + 1L) * (Math.Abs(a.Y - b.Y) + 1L);

    private IEnumerable<(Vector2DInt A, Vector2DInt B)> GetUniquePairs(List<Vector2DInt> points)
    {
        for (var i = 0; i < points.Count - 1; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                yield return (points[i], points[j]);
            }
        }
    }
}
