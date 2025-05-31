using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D11;

public static class InputParser
{
    public static Dictionary<string, int> BitPositions = new();

    public static (List<Floor>, Dictionary<string, int>) ParseInput(PuzzleInput input)
    {
        BitPositions.Clear();
        var floors = input.AsLines().Select(ParseLine).ToList();
        return (floors, BitPositions);
    }

    private static Floor ParseLine(string line)
    {
        var startPos = line.IndexOf(" contains ") + 9;
        var length = line.Length - startPos - 1;
        var lineItems = line.Substring(startPos, length)
            .Replace(" and ", ",")
            .Replace("a ", "")
            .Replace("-compatible", "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(item => item.Trim())
            .Where(item => !string.IsNullOrWhiteSpace(item) && item != "nothing relevant")
            .Select(item => item.Split(' '))
            .ToList();


        // Create bitmasks for microchips and generators
        var microchips = 0u;
        var generators = 0u;
        foreach (var item in lineItems)
        {
            var name = item[0];
            var isMicrochip = item[1] == "microchip";
            if (!BitPositions.ContainsKey(name))
                BitPositions[name] = BitPositions.Count;

            if (isMicrochip)
            {
                microchips |= (uint)1 << BitPositions[name];
            }
            else
            {
                generators |= (uint)1 << BitPositions[name];
            }
        }

        return new Floor(microchips, generators);
    }
}
