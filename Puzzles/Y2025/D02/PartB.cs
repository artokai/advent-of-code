using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D02;

[PuzzleInfo(year: 2025, day: 2, part: 2, title: "Gift Shop")]
public class PartB : SolverBase
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
        for (var id = range.Start; id <= range.End; id++)
        {
            if (IsInvalid(id))
            {
                yield return id;
            }
        }
    }

    private bool IsInvalid(long id)
    {
        var idStr = id.ToString();
        var length = idStr.Length;

        for (var subLength = length / 2; subLength > 0; subLength--)
        {
            if (length % subLength != 0)
                continue;

            var repetitions = length / subLength;
            var repeatingPart = idStr.Substring(0, subLength);
            var repeated = string.Concat(Enumerable.Repeat(repeatingPart, repetitions));
            if (idStr == repeated)
                return true;
        }

        return false;
    }
}
