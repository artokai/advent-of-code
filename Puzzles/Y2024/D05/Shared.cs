namespace Artokai.AOC.Puzzles.Y2024.D05;

public static class Shared
{
    public static Comparer<int> CreateComparer(IEnumerable<(int, int)> sortRules)
    {
        var comparer = Comparer<int>.Create((x, y) =>
        {
            var ltRule = sortRules.Where(r => r.Item1 == x && r.Item2 == y).FirstOrDefault();
            if (ltRule != default) { return -1; }
            var gtRule = sortRules.Where(r => r.Item1 == y && r.Item2 == x).FirstOrDefault();
            if (gtRule != default) { return 1; }
            return 0;
        });
        return comparer;
    }
}