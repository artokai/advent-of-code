using System.Diagnostics.CodeAnalysis;

namespace Artokai.AOC.Puzzles.Y2025.D02;

public class ProductIdRange : IParsable<ProductIdRange>
{
    public long Start { get; private set; }
    public long End { get; private set; }

    public ProductIdRange(long start, long end)
    {
        Start = start;
        End = end;
    }

    public override string ToString() => $"{Start}-{End}";

    public static ProductIdRange Parse(string s, IFormatProvider? provider = null)
    {
        var parts = s.Split('-', 2, StringSplitOptions.TrimEntries);
        return new ProductIdRange(long.Parse(parts[0]), long.Parse(parts[1]));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ProductIdRange result)
    {
        if (s == null)
        {
            result = null;
            return false;
        }

        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}
