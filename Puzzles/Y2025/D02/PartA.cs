using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D02;

[PuzzleInfo(year: 2025, day: 2, part: 1, title: "Gift Shop")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var ranges = Input.AsSingleLine()
            .Split(',', StringSplitOptions.TrimEntries)
            .Select(rangeStr => ProductIdRange.Parse(rangeStr))
            .ToList();
        return ranges.SelectMany(FindInvalidProductIdsInRange).Sum().ToString();
    }

    private IEnumerable<long> FindInvalidProductIdsInRange(ProductIdRange range)
    {
        var startStr = range.Start.ToString();
        var endStr = range.End.ToString();
        var length = startStr.Length;

        if (endStr.Length != length)
        {
            // Split the range in two halfs and process each half separately
            var range1End = (long) Math.Pow(10, length) - 1;
            var range1 = FindInvalidProductIdsInRange(new ProductIdRange(range.Start, range1End));
            foreach (var item in range1) yield return item;

            var range2 = FindInvalidProductIdsInRange(new ProductIdRange(range1End + 1, range.End));
            foreach (var item in range2) yield return item;

            yield break;
        }

        // Only even length numbers can be invalid
        if (length % 2 != 0)
        {
            yield break;
        }

        var halfStart = long.Parse(startStr.Substring(0, length / 2));
        var halfEnd = long.Parse(endStr.Substring(0, length / 2)) + 1;
        for (var s = halfStart; s <= halfEnd; s++)
        {
            var candidate = s * (long)Math.Pow(10, length / 2) + s;
            if (candidate >= range.Start && candidate <= range.End)
            {
                yield return candidate;
            }
        }
    }
}
