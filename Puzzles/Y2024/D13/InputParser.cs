using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D13;

public static class InputParser
{
    public static List<Machine> Parse(PuzzleInput input)
    {
        var lines = input.AsLines();
        var machines = new List<Machine>();

        var buttonRe = new Regex(@"X(?<deltaX>[+-]?\d+),\s*Y(?<deltaY>[+-]?\d+)");
        var targetRe = new Regex(@"X=(?<targetX>-?\d+),\s*Y=(?<targetY>-?\d+)");

        for (var i = 0; i < lines.Count; i += 4)
        {
            var m = new Machine();

            var gA = buttonRe.Match(lines[i]);
            m.deltaXforA = double.Parse(gA.Groups["deltaX"].Value);
            m.deltaYforA = double.Parse(gA.Groups["deltaY"].Value);

            var gB = buttonRe.Match(lines[i + 1]);
            m.deltaXforB = double.Parse(gB.Groups["deltaX"].Value);
            m.deltaYforB = double.Parse(gB.Groups["deltaY"].Value);

            var gT = targetRe.Match(lines[i + 2]);
            m.targetX = double.Parse(gT.Groups["targetX"].Value);
            m.targetY = double.Parse(gT.Groups["targetY"].Value);

            machines.Add(m);
        }
        return machines;
    }
}
