using System.Security.Cryptography;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D04;

[PuzzleInfo(year: 2015, day: 4, part: 2, title: "The Ideal Stocking Stuffer")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var input = Input.AsSingleLine();
        using var md5 = MD5.Create();
        for (int i = 0; i < int.MaxValue; i++)
        {
            var hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input + i));
            if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
            {
                return i.ToString();
            }
        }
        return "Not found";
    }
}
