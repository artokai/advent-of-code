using Artokai.AOC.Core;
using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2015.D06;

[PuzzleInfo(year: 2015, day: 6, part: 1, title: "Probably a Fire Hazard")]
public class PartA : SolverBase
{
    private bool[,] lights = new bool[1000, 1000];

    public override string Solve()
    {
        var lights = new bool[1000, 1000];
        var lines = Input.AsLines();
        var re = new Regex(@"(?<action>turn on|turn off|toggle) (?<startX>\d+),(?<startY>\d+) through (?<endX>\d+),(?<endY>\d+)");
        foreach (var line in lines)
        {
            var m = re.Match(line);
            if (!m.Success)
            {
                throw new Exception($"Invalid input: {line}");
            }

            var action = m.Groups["action"].Value;
            var startX = int.Parse(m.Groups["startX"].Value);
            var startY = int.Parse(m.Groups["startY"].Value);
            var endX = int.Parse(m.Groups["endX"].Value);
            var endY = int.Parse(m.Groups["endY"].Value);

            switch (action)
            {
                case "turn on":
                    Set(startX, startY, endX, endY, (_) => true);
                    break;
                case "turn off":
                    Set(startX, startY, endX, endY, (_) => false);
                    break;
                case "toggle":
                    Set(startX, startY, endX, endY, (state) => !state);
                    break;
            }
        }

        var result = CountLights();
        return result.ToString();
    }

    private void Set(int startX, int startY, int endX, int endY, Func<bool, bool> action)
    {
        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                lights[x, y] = action(lights[x, y]);
            }
        }
    }

    private int CountLights()
    {
        var count = 0;
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                if (lights[x, y])
                {
                    count++;
                }
            }
        }

        return count;
    }
}
