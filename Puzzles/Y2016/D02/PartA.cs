using System.Text;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D02;

[PuzzleInfo(year: 2016, day: 2, part: 1, title: "Bathroom Security")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var result = new StringBuilder();
        var position = new Vector2DInt(1, 1);
        foreach (var line in lines)
        {
            position = GetNewPosition(line, position);
            result.Append(GetKey(position));
        }
        return result.ToString();
    }

    private Vector2DInt GetNewPosition(string line, Vector2DInt position)
    {
        foreach (var c in line)
        {
            position = c switch
            {
                'U' => new Vector2DInt(position.X, Math.Max(0, position.Y - 1)),
                'D' => new Vector2DInt(position.X, Math.Min(2, position.Y + 1)),
                'L' => new Vector2DInt(Math.Max(0, position.X - 1), position.Y),
                'R' => new Vector2DInt(Math.Min(2, position.X + 1), position.Y),
                _ => position
            };
        }
        return position;
    }

    private string GetKey(Vector2DInt position) => (3 * position.Y + position.X + 1).ToString();
}
