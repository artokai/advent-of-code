using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D10;

[PuzzleInfo(year: 2017, day: 10, part: 1, title: "Knot Hash")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lengths = Input
            .AsSingleLine()
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray();
        var list = Enumerable.Range(0, 256).ToArray();
        var index = 0;
        var skipSize = 0;

        foreach (var length in lengths)
        {
            list = CircularReverse(list, index, length);
            index = (index + length + skipSize) % list.Length;
            skipSize++;
        }

        return (list[0] * list[1]).ToString();
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
}
