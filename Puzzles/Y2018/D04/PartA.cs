using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D04;

[PuzzleInfo(year: 2018, day: 4, part: 1, title: "Repose Record")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var schedules = ScheduleParser.Parse(Input);
        var selectedSchedule = schedules.MaxBy(kvp => kvp.Value.Sum());
        var selectedGuardId = selectedSchedule.Key;
        var maxMinute = selectedSchedule.Value.IndexOf(selectedSchedule.Value.Max());
        var result = selectedGuardId * maxMinute;
        return result.ToString();
    }
}
