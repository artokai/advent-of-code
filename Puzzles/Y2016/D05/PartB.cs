using System.Security.Cryptography;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D05;

[PuzzleInfo(year: 2016, day: 5, part: 2, title: "How About a Nice Game of Chess?")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var doorId = Input.AsSingleLine();
        var index = 0;
        var passwordChars = new char[8];
        var lettersFound = 0;
        while (lettersFound < 8)
        {
            var hash = MD5.HashData(System.Text.Encoding.UTF8.GetBytes(doorId + index));
            var beginningAsHex = hash
                .Take(4)
                .Select(b => b.ToString("x2"))
                .Aggregate((a, b) => a + b);
            if (beginningAsHex.StartsWith("00000"))
            {
                var positionChar = beginningAsHex[5];
                if (positionChar >= '0' && positionChar <= '7')
                {
                    var position = int.Parse(positionChar.ToString());
                    if (passwordChars[position] == '\0')
                    {
                        passwordChars[position] = beginningAsHex[6];
                        lettersFound = passwordChars.Count(c => c != '\0');
                    }
                }
            }
            index++;
        }
        return new string(passwordChars);
    }
}
