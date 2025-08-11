namespace Artokai.AOC.Puzzles.Y2016.D18;

public static class Solver
{
    public static int Solve(string input, int iterations)
    {
        var safeCount = 0;
        var row = input.Select(c => c == '^').ToArray();
        for (var i = 0; i < iterations; i++)
        {
            safeCount += row.Select(c => c ? 0 : 1).Sum();
            row = GetNextRow(row);
        }
        return safeCount;
    }

    private static bool[] GetNextRow(bool[] row)
    {
        var next = new bool[row.Length];
        for (var i = 0; i < row.Length; i++)
        {
            var left = i > 0 ? row[i - 1] : false;
            var right = i < row.Length - 1 ? row[i + 1] : false;
            next[i] = left ^ right;
        }
        return next;
    }
}
