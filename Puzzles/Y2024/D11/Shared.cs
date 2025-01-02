namespace Artokai.AOC.Puzzles.Y2024.D11;

public static class Shared
{
    public static long Simulate(IEnumerable<long> blinkers, int maxIters)
    {
        long result = 0;
        var cache = new Dictionary<string, long>();
        foreach (var blinker in blinkers)
        {
            result += SimulateRecursive(blinker, maxIters, maxIters, cache);
        }
        return result;
    }

    private static long SimulateRecursive(long blinker, int maxIters, int iters, Dictionary<string, long> cache)
    {
        if (iters == 0)
        {
            return 1;
        }

        var cacheKey = $"{iters}_{blinker}";
        if (cache.ContainsKey(cacheKey))
        {
            return cache[cacheKey];
        }

        if (blinker == 0)
        {
            var r = SimulateRecursive(1, maxIters, iters - 1, cache);
            cache[cacheKey] = r;
            return r;
        }

        var strBlinker = blinker.ToString();
        if (strBlinker.Length % 2 == 0)
        {
            long r = 0;
            var midPoint = strBlinker.Length / 2;
            var a = long.Parse(strBlinker.Substring(0, midPoint));
            r += SimulateRecursive(a, maxIters, iters - 1, cache);
            var b = long.Parse(strBlinker.Substring(midPoint));
            r += SimulateRecursive(b, maxIters, iters - 1, cache);
            cache[cacheKey] = r;
            return r;
        }

        var r3 = SimulateRecursive(blinker * 2024, maxIters, iters - 1, cache);
        cache[cacheKey] = r3;
        return r3;
    }
}
