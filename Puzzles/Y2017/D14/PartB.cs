using System.Collections;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D14;

[PuzzleInfo(year: 2017, day: 14, part: 2, title: "Disk Defragmentation")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var seed = Input.AsSingleLine();
        var rows = Enumerable.Range(0, 128)
            .Select(i => $"{seed}-{i}")
            .Select(KnotHash.ComputeHash)
            .Select(bytes =>
            {
                // Note: BitArray stores bits in LSB order,
                // so we need to reverse the bits
                var array = new BitArray(bytes);
                for (var i = 0; i < array.Length; i += 8)
                {
                    var msbFirst = new bool[8];
                    for (var j = 0; j < 8; j++)
                        msbFirst[j] = array[i + (7 - j)];
                    for (var j = 0; j < 8; j++)
                        array[i + j] = msbFirst[j];
                }
                return array;
            })
            .ToList();

        var groupId = 0;
        var groupMap = new int[128, 128];
        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                if (!rows[y][x]) continue;
                if (groupMap[y, x] != 0) continue;

                groupId++;
                FloodFill(rows, groupMap, y, x, groupId);
            }
        }

        return groupId.ToString();
    }

    private void FloodFill(List<BitArray> rows, int[,] groupMap, int y, int x, int groupId)
    {
        if (y < 0 || y >= 128 || x < 0 || x >= 128) return;
        if (!rows[y][x]) return;
        if (groupMap[y, x] != 0) return;

        groupMap[y, x] = groupId;
        FloodFill(rows, groupMap, y - 1, x, groupId);
        FloodFill(rows, groupMap, y + 1, x, groupId);
        FloodFill(rows, groupMap, y, x - 1, groupId);
        FloodFill(rows, groupMap, y, x + 1, groupId);
    }
}
