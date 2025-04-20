using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2016.D04;

public class Room
{
    public string EncryptedName { get; set; }
    public int SectorId { get; set; }
    public string Checksum { get; set; }

    public static Room Parse(string value)
    {
        var re = new Regex(@"(?<name>[a-z-]+)-(?<sectorId>\d+)\[(?<checksum>[a-z]+)\]");
        var match = re.Match(value);
        if (!match.Success)
            throw new ArgumentException($"Invalid room format: {value}");
        var name = match.Groups["name"].Value;
        var sectorId = int.Parse(match.Groups["sectorId"].Value);
        var checksum = match.Groups["checksum"].Value;
        return new Room(name, sectorId, checksum);
    }

    public string CalculateChecksum()
    {
        var letterCounts = new Dictionary<char, int>();
        foreach (var c in EncryptedName)
        {
            if (c >= 'a' && c <= 'z')
            {
                var cnt = letterCounts.GetValueOrDefault(c, 0);
                letterCounts[c] = cnt + 1;
            }
        }

        var sortedLetters = letterCounts
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Select(kvp => kvp.Key)
            .Take(5);

        return new string(sortedLetters.ToArray());
    }

    public Room(string name, int sectorId, string checksum)
    {
        EncryptedName = name;
        SectorId = sectorId;
        Checksum = checksum;
    }
}
