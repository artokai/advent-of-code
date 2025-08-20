using System.Collections;
using Artokai.AOC.Core;
using Artokai.AOC.Core.CollectionExtensions;

namespace Artokai.AOC.Puzzles.Y2017.D14;

[PuzzleInfo(year: 2017, day: 14, part: 1, title: "Disk Defragmentation")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        // Note: BitArray stores bits in LSB order,
        // but that does not matter when we're just counting
        // used cells
        var seed = Input.AsSingleLine();
        return Enumerable.Range(0, 128)
            .Select(i => $"{seed}-{i}")
            .Select(KnotHash.ComputeHash)
            .Select(bytes => new BitArray(bytes))
            .SelectMany(bits => bits.Cast<bool>())
            .Count(b => b)
            .ToString();
    }
}
