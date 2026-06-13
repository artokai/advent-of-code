using System.Runtime.InteropServices;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D02;

[PuzzleInfo(year: 2018, day: 2, part: 2, title: "Inventory Management System")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var ids = Input.AsLines();
        for (var i = 0; i < ids.Count - 2; i++)
        {
            var a = ids[i];
            for (var j = i + 1; j < ids.Count - 1; j++)
            {
                var b = ids[j];
                var common = string.Join("", a.Where((c, idx) => b[idx] == c));
                if (common.Length == a.Length - 1)
                {
                    return common;
                }
            }
        }
        return "Not found!";
    }
}
