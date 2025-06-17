using System.Collections;
using System.Text;

namespace Artokai.AOC.Core.CollectionExtensions;

public static class BitArrayExtensions
{
    public static string ToHumanReadableString(this BitArray bitArray)
    {
        var sb = new StringBuilder(bitArray.Length);
        for (var i = 0; i < bitArray.Length; i++)
        {
            sb.Append(bitArray[i] ? '1' : '0');
        }
        return sb.ToString();
    }

    public static BitArray FromHumanReadableString(string s)
    {
        var bits = new bool[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '1') bits[i] = true;
            else if (s[i] == '0') bits[i] = false;
            else throw new ArgumentException("Input string must contain only '0' or '1'.");
        }
        return new BitArray(bits);
    }

    public static BitArray Take(this BitArray source, int n)
    {
        var result = new BitArray(n);
        for (var i = 0; i < n && i < source.Length; i++)
        {
            result[i] = source[i];
        }
        return result;
    }
}
