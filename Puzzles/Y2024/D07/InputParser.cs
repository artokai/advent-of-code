namespace Artokai.AOC.Puzzles.Y2024.D07;

public static class InputParser
{
    public static List<(long, List<long>)> ParseInput(List<string> lines)
    {
        var parsed = new List<(long, List<long>)>();
        foreach (var line in lines)
        {
            var splitResults = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var result = long.Parse(splitResults[0]);
            var parts = splitResults[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => long.Parse(s)).ToList();
            parsed.Add((result, parts));
        }
        return parsed;
    }
}
