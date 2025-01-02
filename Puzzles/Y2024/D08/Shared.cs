namespace Artokai.AOC.Puzzles.Y2024.D08;

public static class InputParser
{
    public static AntennaMap ParseInput(List<string> lines)
    {
        var width = lines[0].Length;
        var height = lines.Count;
        var antennas = new List<Antenna>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] != '.')
                {
                    antennas.Add(new Antenna(x, y, lines[y][x]));
                }
            }
        }

        return new AntennaMap { Width = width, Height = height, Antennas = antennas };
    }
}

public class AntennaMap
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Antenna> Antennas { get; set; } = [];
}

public class Antenna
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Type { get; set; }

    public Antenna(int x, int y, char type)
    {
        X = x;
        Y = y;
        Type = type;
    }
}

public class Antinode
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Type { get; set; }

    public Antinode(int x, int y, char type)
    {
        X = x;
        Y = y;
        Type = type;
    }
}
