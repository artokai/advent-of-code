using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D22;

[PuzzleInfo(year: 2017, day: 22, part: 1, title: "Sporifica Virus")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var infected = InputParser.Parse(Input);
        var position = new Vector2DInt(0, 0);
        var direction = Vector2DInt.Up;
        var infectionCount = 0;

        for (var i = 0; i < 10_000; i++)
        {
            if (infected.Contains(position))
            {
                direction = direction.TurnRight();
                infected.Remove(position);
            }
            else
            {
                direction = direction.TurnLeft();
                infected.Add(position);
                infectionCount++;
            }
            position += direction;
        }
        return infectionCount.ToString();
    }
}
