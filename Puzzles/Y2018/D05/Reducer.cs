using System.Text;

namespace Artokai.AOC.Puzzles.Y2018.D05;

public static class Reducer
{
    public const int UPPER_LOWER_DISTANCE = 'a' - 'A';

    public static string Reduce(string input)
    {
        var removed = new bool[input.Length];
        var left = 0;
        var right = 1;
        while (left < input.Length && right < input.Length)
        {
            var leftChar = input[left];
            var rightChar = input[right];
            if (Math.Abs(leftChar - rightChar) == UPPER_LOWER_DISTANCE)
            {
                removed[left] = true;
                removed[right] = true;
                while (left >= 0 && removed[left]) { left--; }
                if (left < 0) { left = right + 1; }
                right++;
                if (right == left) { right++; }
            }
            else
            {
                left = right;
                right = left + 1;
            }
        }

        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            if (!removed[i])
            {
                sb.Append(input[i]);
            }
        }
        return sb.ToString();
    }
}
