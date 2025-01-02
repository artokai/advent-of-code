using Artokai.AOC.Core;
using System.Numerics;

namespace Artokai.AOC.Puzzles.Y2024.D08;

[PuzzleInfo(year: 2024, day: 8, part: 2, title: "Resonant Collinearity")]
public class PartB : SolverBase
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

                    if (deltaX == 0)
                    {
                        deltaY = 1;
                    }
                    else if (deltaY == 0)
                    {
                        deltaX = 1;
                    }
                    else
                    {
                        var gcd = (int)BigInteger.GreatestCommonDivisor(deltaX, deltaY);
                        if (gcd != 1)
                        {
                            deltaX /= gcd;
                            deltaY /= gcd;
                        }
                    }

                    var isInMap = (Antinode node) => node.X >= 0 && node.X < map.Width && node.Y >= 0 && node.Y < map.Height;
                    var alreadyAdded = (Antinode node) => antinodes.Any(a => a.X == node.X && a.Y == node.Y);

                    var n = 0;
                    var isOutside = false;
                    do
                    {
                        var antinode = new Antinode(antennaA.X + n * deltaX, antennaA.Y + n * deltaY, group.Key);
                        isOutside = !isInMap(antinode);
                        if (!isOutside && !alreadyAdded(antinode))
                        {
                            antinodes.Add(antinode);
                        }
                        n++;
                    } while (!isOutside);

                    n = 0;
                    isOutside = false;
                    do
                    {
                        var antinode = new Antinode(antennaA.X + n * deltaX, antennaA.Y + n * deltaY, group.Key);
                        isOutside = !isInMap(antinode);
                        if (!isOutside && !alreadyAdded(antinode))
                        {
                            antinodes.Add(antinode);
                        }
                        n--;
                    } while (!isOutside);
                }
            }
        }

        return antinodes.Count.ToString();
    }
}
