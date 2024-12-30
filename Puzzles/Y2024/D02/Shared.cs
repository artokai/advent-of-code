namespace Artokai.AOC.Puzzles.Y2024.D02;

public static class Shared
{
    public static bool IsSafe(List<int> report)
    {
        if (report.Count == 1) { return true; }

        var prevIncreasing = report[1] > report[0];
        for (int i = 1; i < report.Count; i++)
        {
            var isIncreasing = report[i] > report[i - 1];
            if (prevIncreasing != isIncreasing)
            {
                return false;
            }
            var diff = Math.Abs(report[i] - report[i - 1]);
            if (diff < 1 || diff > 3)
            {
                return false;
            }
        }

        return true;
    }
}