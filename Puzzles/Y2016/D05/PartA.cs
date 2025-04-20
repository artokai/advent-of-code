using System.Security.Cryptography;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D05;

[PuzzleInfo(year: 2016, day: 5, part: 1, title: "How About a Nice Game of Chess?")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var doorId = Input.AsSingleLine();
        var index = 0;
        var password = string.Empty;
        while (password.Length < 8)
        {
            var hash = MD5.HashData(System.Text.Encoding.UTF8.GetBytes(doorId + index));
            var beginningAsHex = hash
                .Take(3)
                .Select(b => b.ToString("x2"))
                .Aggregate((a, b) => a + b);
            if (beginningAsHex.StartsWith("00000"))
            {
                password += beginningAsHex[5];
            }
            index++;
        }
        return password;
    }
}
