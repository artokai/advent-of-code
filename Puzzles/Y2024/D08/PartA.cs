using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D08;

[PuzzleInfo(year: 2024, day: 8, part: 1, title: "Resonant Collinearity")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var map = InputParser.ParseInput(Input.AsLines());
        var antinodes = new List<Antinode>();
        var groups = map.Antennas.GroupBy(a => a.Type);

        foreach (var group in groups)
        {
            for (var a = 0; a < group.Count() - 1; a++)
            {
                for (var b = a + 1; b < group.Count(); b++)
                {
                    var antennaA = group.ElementAt(a);
                    var antennaB = group.ElementAt(b);
                    var deltaX = antennaA.X - antennaB.X;
                    var deltaY = antennaA.Y - antennaB.Y;

                    var antinodeA = new Antinode(antennaA.X + deltaX, antennaA.Y + deltaY, group.Key);
                    if (antinodeA.X >= 0 && antinodeA.X < map.Width && antinodeA.Y >= 0 && antinodeA.Y < map.Height)
                    {
                        if (!antinodes.Any(a => a.X == antinodeA.X && a.Y == antinodeA.Y))
                        {
                            antinodes.Add(antinodeA);
                        }
                    }

                    var antinodeB = new Antinode(antennaB.X - deltaX, antennaB.Y - deltaY, group.Key);
                    if (antinodeB.X >= 0 && antinodeB.X < map.Width && antinodeB.Y >= 0 && antinodeB.Y < map.Height)
                    {
                        if (!antinodes.Any(a => a.X == antinodeB.X && a.Y == antinodeB.Y))
                        {
                            antinodes.Add(antinodeB);
                        }
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }
}
