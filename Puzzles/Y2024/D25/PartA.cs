using Artokai.AOC.Core;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2024.D25;

[PuzzleInfo(year: 2024, day: 25, part: 1, title: "Code Chronicle")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var shapeInputs = Input.SplitOnEmptyLines();
        var shapes = shapeInputs.Select(ParseShape).ToList();
        var keys = shapes.Where(s => s.IsKey).ToList();
        var locks = shapes.Where(s => !s.IsKey).ToList();

        var result = 0L;
        foreach (var l in locks) {
            foreach (var k in keys) {
                if (IsMatch(l, k)) {
                    result++;
                }
            }
        }
        return result.ToString();
    }

    private Shape ParseShape(PuzzleInput input)
    {
        var lines = input.AsLines();    
        var shapeWidth = lines[0].Length;
        var shapeHeight = lines.Count;

        var isKey = lines[0].All(c => c == '.');
        var contour = new int[shapeWidth];
        for (var x = 0; x < shapeWidth; x++) {
            var curHeight = 0;
            for (var y = 0; y < shapeHeight; y++) {
                var c = lines[y][x];
                if (c != (isKey ? '.' : '#')) {
                    break;
                }
                curHeight++;
            }
            curHeight = isKey ? shapeHeight - curHeight -1 : curHeight-1;
            contour[x] = curHeight;
        }
        return new Shape(isKey, shapeHeight, contour);
    }

    private bool IsMatch(Shape a, Shape b)
    {
        if (a.IsKey == b.IsKey) { return false; }
        var w = a.Countour.Length;
        for (var i = 0; i < w; i++) {
            if (a.Countour[i] + b.Countour[i] > a.Height - 2) {
                return false;
            }
        }
        return true;
    }
}

public record struct Shape (bool IsKey, int Height, int[] Countour);