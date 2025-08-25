using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D22;

[PuzzleInfo(year: 2017, day: 22, part: 2, title: "Sporifica Virus")]
public class PartB : SolverBase
{
    private const int Clean = 0;
    private const int Weakened = 1;
    private const int Infected = 2;
    private const int Flagged = 3;

    public override string Solve()
    {
        var infected = InputParser.Parse(Input);
        var states = new Dictionary<Vector2DInt, int>();
        infected.ToList().ForEach(v => states[v] = Infected);

        var position = new Vector2DInt(0, 0);
        var direction = Vector2DInt.Up;
        var infectionCount = 0;
        for (var i = 0; i < 10_000_000; i++)
        {
            var currentState = states.GetValueOrDefault(position, Clean);
            var nextState = (currentState + 1) % 4;
            if (nextState == Clean)
            {
                states.Remove(position);
            }
            else
            {
                states[position] = nextState;
            }

            if (nextState == Infected)
            {
                infectionCount++;
            }

            direction = currentState switch
            {
                Clean => direction.TurnLeft(),
                Weakened => direction,
                Infected => direction.TurnRight(),
                Flagged => direction.Turn180(),
                _ => throw new InvalidOperationException("Invalid state")
            };
            position += direction;
        }

        return infectionCount.ToString();
    }
}
