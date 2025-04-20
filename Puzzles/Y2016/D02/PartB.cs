using System.Text;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D02;

[PuzzleInfo(year: 2016, day: 2, part: 2, title: "Bathroom Security")]
public class PartB : SolverBase
{
    public Dictionary<Vector2DInt, string> Keypad = new Dictionary<Vector2DInt, string>
    {
        { new Vector2DInt(2, 0), "1" },

        { new Vector2DInt(1, 1), "2" },
        { new Vector2DInt(2, 1), "3" },
        { new Vector2DInt(3, 1), "4" },

        { new Vector2DInt(0, 2), "5" },
        { new Vector2DInt(1, 2), "6" },
        { new Vector2DInt(2, 2), "7" },
        { new Vector2DInt(3, 2), "8" },
        { new Vector2DInt(4, 2), "9" },

        { new Vector2DInt(1, 3), "A" },
        { new Vector2DInt(2, 3), "B" },
        { new Vector2DInt(3, 3), "C" },

        { new Vector2DInt(2, 4), "D" },
    };

    public override string Solve()
    {
        var lines = Input.AsLines();
        var result = new StringBuilder();
        var position = new Vector2DInt(0, 2);
        foreach (var line in lines)
        {
            position = GetNewPosition(line, position);
            result.Append(Keypad[position]);
        }
        return result.ToString();
    }

    private Vector2DInt GetNewPosition(string line, Vector2DInt position)
    {
        foreach (var c in line)
        {
            var positionCandidate = c switch
            {
                'U' => new Vector2DInt(position.X, Math.Max(0, position.Y - 1)),
                'D' => new Vector2DInt(position.X, Math.Min(4, position.Y + 1)),
                'L' => new Vector2DInt(Math.Max(0, position.X - 1), position.Y),
                'R' => new Vector2DInt(Math.Min(4, position.X + 1), position.Y),
                _ => position
            };

            if (Keypad.ContainsKey(positionCandidate))
            {
                position = positionCandidate;
            }
        }
        return position;
    }
}
