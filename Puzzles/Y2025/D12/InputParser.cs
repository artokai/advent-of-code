using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2025.D12;

public static class InputParser
{
    public static (List<Present> presents, List<Area> areas) Parse(PuzzleInput input)
    {
        var presents = new List<Present>();
        var areas = new List<Area>();
        var enumerator = input.AsLines().GetEnumerator();
        while (enumerator.MoveNext())
        {
            var line = enumerator.Current;
            if (line.Contains("x"))
            {
                areas.Add(ParseArea(ref enumerator));
            }
            else
            {
                presents.Add(ParsePresent(ref enumerator));
            }
        }
        return (presents, areas);
    }

    private static Present ParsePresent(ref List<string>.Enumerator enumerator)
    {
        var line = enumerator.Current;
        var id = int.Parse(line[..^1]);
        var width = 0;
        var height = 0;
        var size = 0;
        while (!string.IsNullOrEmpty(line))
        {
            enumerator.MoveNext();
            line = enumerator.Current;
            if (!string.IsNullOrEmpty(line))
            {
                height++;
                width = Math.Max(width, line.Length);
                size += line.Count(c => c == '#');
            }
        }
        return new Present(id, size);
    }

    private static Area ParseArea(ref List<string>.Enumerator enumerator)
    {
        var line = enumerator.Current;
        var parts = line.Split(' ');
        var dimensions = parts[0].Split('x');
        var width = int.Parse(dimensions[0]);
        var height = int.Parse(dimensions[1][..^1]);
        var requirements = parts.Skip(1).Select(int.Parse).ToList();
        return new Area(width, height, requirements);
    }
}

public record Present(int id, int Size);

public record Area(int Width, int Height, List<int> RequiredPresents);
