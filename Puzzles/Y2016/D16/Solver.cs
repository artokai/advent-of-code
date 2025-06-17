using System.Collections;
using Artokai.AOC.Core.CollectionExtensions;

namespace Artokai.AOC.Puzzles.Y2016.D16;

public static class Solver
{
    public static string Solve(string input, int discSize)
    {
        var inputBits = BitArrayExtensions.FromHumanReadableString(input);
        var dragonCurve = DragonCurve(inputBits, discSize);
        var trimmedDragonCurve = dragonCurve.Take(discSize);
        var checksum = Checksum(trimmedDragonCurve);
        return checksum.ToHumanReadableString();
    }

    public static BitArray DragonCurve(BitArray seed, int minLength)
    {
        var endPart = ReverseAndInvert(seed);
        var dragonCurve = Combine(seed, endPart, false);
        return (dragonCurve.Length < minLength)
            ? DragonCurve(dragonCurve, minLength)
            : dragonCurve;
    }

    public static BitArray ReverseAndInvert(BitArray input)
    {
        var result = new BitArray(input.Length);
        for (var i = 0; i < input.Length; i++)
        {
            result[i] = !input[input.Length - 1 - i];
        }
        return result;
    }

    public static BitArray Combine(BitArray a, BitArray b, bool separator)
    {
        var combinedLength = a.Length + b.Length + 1;
        var halfPoint = a.Length;
        var combined = new BitArray(combinedLength);
        for (var i = 0; i < combinedLength; i++)
        {
            if (i < halfPoint)
                combined[i] = a[i];
            else if (i == halfPoint)
                combined[i] = false;
            else
                combined[i] = b[i - halfPoint - 1];
        }
        return combined;
    }

    public static BitArray Checksum(BitArray data)
    {
        var checksum = new List<bool>();
        for (var i = 0; i < data.Length - 1; i = i + 2)
        {
            var b1 = data[i];
            var b2 = data[i + 1];
            checksum.Add(b1 == b2);
        }

        var checksumAsBitArray = new BitArray(checksum.ToArray());
        return (checksumAsBitArray.Length % 2 == 0)
            ? Checksum(checksumAsBitArray)
            : checksumAsBitArray;
    }
}
