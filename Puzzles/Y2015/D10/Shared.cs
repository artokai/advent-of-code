using System.Text;

namespace Artokai.AOC.Puzzles.Y2015.D10;

public static class Shared
{
    public static string Generate(string input, int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            input = Generate(input);
        }
        return input;
    }

    public static string Generate(string input)
    {
        var sb = new StringBuilder();
        var cnt = 1;
        for (var i = 1; i < input.Length; i++)
        {
            if (input[i - 1] == input[i])
            {
                cnt++;
            }
            else
            {
                sb.Append(cnt);
                sb.Append(input[i - 1]);
                cnt = 1;
            }
        }
        sb.Append(cnt);
        sb.Append(input[^1]);
        return sb.ToString();
    }
}
