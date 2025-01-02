using Artokai.AOC.Core;
using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2015.D06;

[PuzzleInfo(year: 2015, day: 6, part: 2, title: "Probably a Fire Hazard")]
public class PartB : SolverBase
{
    private int[,] lights = new int[1000, 1000];

    public override string Solve()
    {
        var lights = new int[1000, 1000];
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                lights[x, y] = 0;
            }
        }

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
                    Set(startX, startY, endX, endY, (prev) => prev + 1);
                    break;
                case "turn off":
                    Set(startX, startY, endX, endY, (prev) => prev > 0 ? prev - 1 : 0);
                    break;
                case "toggle":
                    Set(startX, startY, endX, endY, (prev) => prev + 2);
                    break;
            }
        }

        var result = SumBrightness();
        return result.ToString();
    }

    private void Set(int startX, int startY, int endX, int endY, Func<int, int> action)
    {
        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                lights[x, y] = action(lights[x, y]);
            }
        }
    }

    private long SumBrightness()
    {
        var result = 0L;
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                result += lights[x, y];
            }
        }

        return result;
    }
}
