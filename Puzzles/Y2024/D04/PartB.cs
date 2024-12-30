using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D04;

[PuzzleInfo(year: 2024, day: 4, part: 2, title: "Ceres Search")]
public class PartB : SolverBase
{
    private string[] shapes = [
        """
        M.M
        .A.
        S.S
        """.ReplaceLineEndings(""),
        """
        M.S
        .A.
        M.S
        """.ReplaceLineEndings(""),
        """
        S.M
        .A.
        S.M
        """.ReplaceLineEndings(""),
        """
        S.S
        .A.
        M.M
        """.ReplaceLineEndings(""),
    ];

    public override string Solve()
    {
        var input = Input.AsCharTable();
        var result = 0;
        for (var y = 0; y <= input.Length - 3; y++)
        {
            for (var x = 0; x <= input[y].Length - 3; x++)
            {

                var inputBox = string.Concat(input[y..(y + 3)].SelectMany(row => row[x..(x + 3)]));
                result += shapes.Count(s => IsMatch(inputBox, s));
            }
        }
        return result.ToString();
    }

    private bool IsMatch(string input, string shape) =>
        input.Select((c, i) => shape[i] == '.' || shape[i] == c).All(b => b);
}
