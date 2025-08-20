namespace Artokai.AOC.Puzzles.Y2017.D15;

public static class Generator
{
    public static IEnumerable<int> Generate(int seed, int factor, int multitudes = -1)
    {
        if (multitudes > 1 && !IsPowerOfTwo(multitudes))
            throw new ArgumentException("Multitudes must be a power of two", nameof(multitudes));

        long value = seed;
        while (true)
        {
            value = ((value * factor) % 2147483647L);
            if (multitudes < 2 || (value & (multitudes - 1)) == 0)
                yield return (int)value;
        }
    }

    private static Func<int, bool> IsPowerOfTwo = (int value) => value > 0 && (value & (value - 1)) == 0;
}
