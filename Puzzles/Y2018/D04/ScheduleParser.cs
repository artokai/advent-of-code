

using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2018.D04;

public static class ScheduleParser
{
    public static Dictionary<int, int[]> Parse(PuzzleInput input)
    {
        var lines = input.AsLines().Order().ToList();
        var schedules = new Dictionary<int, int[]>();
        var guardId = 0;
        var sleepStart = 0;
        foreach (var line in lines)
        {
            var minute = int.Parse(line.Substring(15, 2));
            var lineType = line.Substring(19, 5).ToLower();
            switch (lineType)
            {
                case "falls":
                    sleepStart = minute;
                    break;
                case "wakes":
                    var sched = schedules[guardId];
                    for (var m = sleepStart; m < minute; m++)
                    {
                        sched[m]++;
                    }
                    break;
                case "guard":
                    var endIndex = line.IndexOf(' ', 26) - 1;
                    guardId = int.Parse(line.Substring(26, endIndex - 25));
                    sleepStart = 0;
                    if (!schedules.ContainsKey(guardId))
                    {
                        schedules.Add(guardId, new int[60]);
                    }
                    break;
                default:
                    throw new Exception("Invalid line: " + line);
            }
        }
        return schedules;
    }
}
