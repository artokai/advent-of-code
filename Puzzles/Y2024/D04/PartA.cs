using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D04;

[PuzzleInfo(year: 2024, day: 4, part: 1, title: "Ceres Search")]
public class PartA : SolverBase
{
    private const string NEEDLE = "XMAS";
    private const string REVERSE_NEEDLE = "SAMX";

    public override string Solve()
    {
        var input = Input.AsCharTable();
        var result = 0;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {

                if (x <= input[0].Length - NEEDLE.Length)
                {
                    var horizontal = new string(input[y][x..(x + NEEDLE.Length)]);
                    result += (horizontal == NEEDLE || horizontal == REVERSE_NEEDLE) ? 1 : 0;
                }

                if (y <= input.Length - NEEDLE.Length)
                {
                    var vertical = new string(input[y..(y + NEEDLE.Length)].Select(row => row[x]).ToArray());
                    result += (vertical == NEEDLE || vertical == REVERSE_NEEDLE) ? 1 : 0;
                }

                if (x <= input[0].Length - NEEDLE.Length && y <= input.Length - NEEDLE.Length)
                {
                    var diagonal_se = new string(input[y..(y + NEEDLE.Length)].Select((row, i) => row[x + i]).ToArray());
                    result += (diagonal_se == NEEDLE || diagonal_se == REVERSE_NEEDLE) ? 1 : 0;
                }

                if (x >= NEEDLE.Length - 1 && y <= input.Length - NEEDLE.Length)
                {
                    var diagonal_sw = new string(input[y..(y + NEEDLE.Length)].Select((row, i) => row[x - i]).ToArray());
                    result += (diagonal_sw == NEEDLE || diagonal_sw == REVERSE_NEEDLE) ? 1 : 0;
                }
            }
        }
        return result.ToString();
    }
}
