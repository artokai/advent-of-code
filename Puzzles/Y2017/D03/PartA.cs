using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D03;

[PuzzleInfo(year: 2017, day: 3, part: 1, title: "Spiral Memory")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        // The bottom right corner of each circle is the squared value
        // of sequential odd numbers. The manhattan distance to
        // the center of this value is n-1.
        //
        // 37  36  35  34  33  32  31
        // 38  17  16  15  14  13  30
        // 39  18   5   4   3  12  29
        // 40  19   6  *1*  2  11  28
        // 41  20   7   8  *9* 10  27   ^
        // 42  21  22  23  24 *25* 26   |
        // 43  44  45  46  47  48 *49* 50

        var target = int.Parse(Input.AsSingleLine());
        var solution = FindDistance(target);
        return solution.ToString();
    }

    public int FindDistance(int target)
    {
        // Find the smallest odd number whose square is smaller or equal to the target.
        var n = 1;
        while ((n * n) < target) n = n + 2;

        // If the target is a perfect square, the distance is simply the square root minus 1.
        if (n * n == target) return n - 1;

        // Find the largest odd number that whose square is less than the target
        n = n - 2;

        // Step a  single step to the right
        var corner = new Vector2DInt(n / 2, n / 2);
        var diff = target - (n * n) - 1;
        var delta = Vector2DInt.Right;
        if (diff == 1) return corner.ManhattanDistance() + 1;

        // Step upwards, then turn left etc...
        var steps = 1;
        var stepsPerSide = n + 1;
        var dir = Vector2DInt.Up;
        while (diff > 0)
        {
            diff--;
            delta += dir;
            steps++;
            if (steps % stepsPerSide == 0)
            {
                dir = dir.TurnLeft();
            }

        }
        var combined = corner + delta;
        return combined.ManhattanDistance() ;
    }
}
