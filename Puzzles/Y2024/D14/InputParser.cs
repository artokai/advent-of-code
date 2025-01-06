using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D14;

public static class InputParser
{
    public static List<Robot> ParseInput(PuzzleInput input)
    {
        var lines = input.AsLines();
        var robots = new List<Robot>();
        var robotRe = new Regex(@"p=(?<x>-?\d+),(?<y>-?\d+)\s+v=(?<dx>-?\d+),(?<dy>-?\d+)");
        for (var i = 0; i < lines.Count; i++)
        {
            var m = robotRe.Match(lines[i]);
            if (m.Success)
            {
                var r = new Robot();
                r.x = int.Parse(m.Groups["x"].Value);
                r.y = int.Parse(m.Groups["y"].Value);
                r.dx = int.Parse(m.Groups["dx"].Value);
                r.dy = int.Parse(m.Groups["dy"].Value);
                robots.Add(r);
            }
            else
            {
                Console.WriteLine($"Failed to parse line {i}: {lines[i]}");
                throw new Exception("Failed to parse input");
            }
        }
        return robots;
    }
}
