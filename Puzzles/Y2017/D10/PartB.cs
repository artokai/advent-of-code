using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D10;

[PuzzleInfo(year: 2017, day: 10, part: 2, title: "Knot Hash")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var lengths = Input.AsSingleLine().ToArray().Select(c => (int)c).ToList();
        lengths.AddRange(new[] { 17, 31, 73, 47, 23 });

        var list = Enumerable.Range(0, 256).ToArray();
        var index = 0;
        var skipSize = 0;
        for (var round = 0; round < 64; round++)
        {
            foreach (var length in lengths)
            {
                list = CircularReverse(list, index, length);
                index = (index + length + skipSize) % list.Length;
                skipSize++;
            }
        }

        var dense = Densify(list);
        var hash = string.Join("", dense.Select(x => x.ToString("x2")));
        return hash;
    }

    private int[] CircularReverse(int[] list, int start, int count)
    {
        var reversed = new int[list.Length];
        for (var i = 0; i < list.Length; i++)
            reversed[i] = list[i];

        for (var i = 0; i < count; i++)
        {
            var src = (start + count - i - 1) % list.Length;
            var dst = (start + i) % list.Length;
            reversed[dst] = list[src];
        }

        return reversed;
    }

    private int[] Densify(int[] list)
    {
        var result = new int[16];
        for (var block = 0; block < 16; block++)
        {
            var blockValue = list.Skip(block * 16).Take(16).Aggregate(0, (x, y) => x ^ y);
            result[block] = blockValue;
        }
        return result;
    }
}
