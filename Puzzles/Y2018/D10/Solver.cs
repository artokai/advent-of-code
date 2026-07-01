using System.Text.RegularExpressions;
using Artokai.AOC.Core.Geometry;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2018.D10;

public static class Solver
{
    private static (List<(Vector2DInt position, Vector2DInt velocity)> lights, BoundingBox boundingBox) ParseInput(PuzzleInput input)
    {
        var maxX = int.MinValue;
        var minX = int.MaxValue;
        var maxY = int.MinValue;
        var minY = int.MaxValue;

        var re = new Regex(@"position=<\s*(?<px>-?\d+),\s*(?<py>-?\d+)\s*>\s*velocity=<\s*(?<vx>-?\d+),\s*(?<vy>-?\d+)\s*>");
        var lights = input.AsLines()
            .Select(line =>
            {
                var m = re.Match(line);
                var position = new Vector2DInt(int.Parse(m.Groups["px"].Value), int.Parse(m.Groups["py"].Value));
                var velocity = new Vector2DInt(int.Parse(m.Groups["vx"].Value), int.Parse(m.Groups["vy"].Value));

                maxX = Math.Max(maxX, position.X);
                minX = Math.Min(minX, position.X);
                maxY = Math.Max(maxY, position.Y);
                minY = Math.Min(minY, position.Y);

                return (position, velocity);
            })
            .ToList();

        var boundingBox = new BoundingBox(minX, minY, maxX - minX, maxY - minY);
        return (lights, boundingBox);
    }

    private static (List<(Vector2DInt position, Vector2DInt velocity)> lights, BoundingBox boundingBox) Step(List<(Vector2DInt position, Vector2DInt velocity)> lights)
    {
        var maxX = int.MinValue;
        var minX = int.MaxValue;
        var maxY = int.MinValue;
        var minY = int.MaxValue;

        var newLights = new List<(Vector2DInt position, Vector2DInt velocity)>();
        foreach (var light in lights)
        {
            var position = light.position + light.velocity;
            newLights.Add((position, light.velocity));

            maxX = Math.Max(maxX, position.X);
            minX = Math.Min(minX, position.X);
            maxY = Math.Max(maxY, position.Y);
            minY = Math.Min(minY, position.Y);
        }

        var boundingBox = new BoundingBox(minX, minY, maxX - minX, maxY - minY);
        return (newLights, boundingBox);
    }

    public static (List<Vector2DInt> lights, Vector2DInt size, int seconds) Solve(PuzzleInput input, int maxIterations)
    {
        var (lights, boundingBox) = ParseInput(input);
        var minIter = -1;
        var minBoundingBox = boundingBox;
        var minLights = lights;

        for (var iter = 1; iter < maxIterations; iter++)
        {
            (lights, boundingBox) = Step(lights);
            if (boundingBox.Size > 0 && boundingBox.Size < minBoundingBox.Size)
            {
                minIter = iter;
                minLights = lights;
                minBoundingBox = boundingBox;
            }
        }

        var final = minLights.Select(l =>
            new Vector2DInt(
                l.position.X - minBoundingBox.X,
                l.position.Y - minBoundingBox.Y
            )
        ).ToList();

        var size = new Vector2DInt(
            minBoundingBox.Width,
            minBoundingBox.Height
        );

        return (final, size, minIter);

    }
}

public record BoundingBox(int X, int Y, int Width, int Height)
{
    public long Size
    {
        get => Width * (long) Height;
    }
}
