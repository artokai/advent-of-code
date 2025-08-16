using System.Collections.Concurrent;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D24;

public record POI(int Id, Vector2DInt Position)
{
    public int X => Position.X;
    public int Y => Position.Y;
    public override string ToString() => $"{Id} at ({X}, {Y})";
};

public class Map
{
    private static readonly Vector2DInt[] Directions =
    [
        Vector2DInt.Up,
        Vector2DInt.Right,
        Vector2DInt.Down,
        Vector2DInt.Left
    ];

    public int Width { get; init; }
    public int Height { get; init; }

    public char[,] Tiles { get; init; } = null!;

    public List<POI> POIs { get; init; } = new();

    public Map(char[,] data)
    {
        Width = data.GetLength(0);
        Height = data.GetLength(1);
        Tiles = data;
        POIs = ExtractPointsOfInterest();
    }

    private List<POI> ExtractPointsOfInterest()
    {
        var pois = new List<POI>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                char tile = Tiles[x, y];
                if (char.IsDigit(tile))
                {
                    pois.Add(new POI(tile - '0', new Vector2DInt(x, y)));
                    Tiles[x, y] = '.';
                }
            }
        }
        return pois.OrderBy(p => p.Id).ToList();
    }

    public Dictionary<int, Dictionary<int, int>> FindAllShortestDistances()
    {
        var allDistances = new ConcurrentDictionary<int, Dictionary<int, int>>();
        var opts = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        Parallel.ForEach(POIs, opts, start =>
        {
            Dictionary<int, int> distances = FindShortestDistances(start);
            allDistances.TryAdd(start.Id, distances);
        });
        return allDistances.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private Dictionary<int, int> FindShortestDistances(POI start)
    {
        var distances = new Dictionary<int, int>();
        var steps = new int[Width * Height];
        Array.Fill(steps, int.MaxValue);

        var queue = new Queue<Vector2DInt>();
        queue.Enqueue(start.Position);
        steps[start.X + start.Y * Width] = 0;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentSteps = steps[current.X + current.Y * Width];
            foreach (var dir in Directions)
            {
                var nexPos = current + dir;
                if (nexPos.X < 0 || nexPos.X >= Width || nexPos.Y < 0 || nexPos.Y >= Height) continue;
                if (Tiles[nexPos.X, nexPos.Y] == '#') continue;
                if (steps[nexPos.X + nexPos.Y * Width] != int.MaxValue) continue;

                var stepsToNextPos = currentSteps + 1;
                var foundPoi = POIs
                    .Where(p => p.Position.Equals(nexPos))
                    .FirstOrDefault();
                if (foundPoi != null)
                {
                    distances[foundPoi.Id] = stepsToNextPos;
                }

                steps[nexPos.X + nexPos.Y * Width] = stepsToNextPos;
                queue.Enqueue(nexPos);
            }
        }
        return distances;
    }
}
