using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D13;

public record State(Vector2DInt Position, int Steps, State? Previous);

public class Solver
{
    private readonly int _favoriteNumber;
    private readonly Vector2DInt _start;
    private readonly Vector2DInt? _target;
    private readonly HashSet<Vector2DInt> _seen;
    public int StepsToTarget { get; private set; } = int.MinValue;
    private int _maxSteps = int.MaxValue;

    public Solver(int favoriteNumber, Vector2DInt start, Vector2DInt? target, int maxSteps = int.MaxValue)
    {
        _favoriteNumber = favoriteNumber;
        _seen = new HashSet<Vector2DInt>();
        _maxSteps = maxSteps;
        _start = start;
        _target = target;
    }

    public void Solve()
    {
        var initialState = new State(_start, 0, null);
        var queue = new Queue<State>();
        _seen.Add(initialState.Position);
        queue.Enqueue(initialState);

        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue();
            var nextPositions = GetNextPositions(currentState.Position);
            foreach (var nextPosition in nextPositions)
            {
                var nextStepCount = currentState.Steps + 1;
                if (nextStepCount > _maxSteps)
                {
                    continue; // Skip positions that exceed the max steps allowed
                }

                if (_target != null && nextPosition == _target)
                {
                    StepsToTarget = nextStepCount;
                    return;
                }

                if (!_seen.Contains(nextPosition))
                {
                    _seen.Add(nextPosition);
                    queue.Enqueue(new State(nextPosition, nextStepCount, currentState));
                }
            }
        }
    }

    private IEnumerable<Vector2DInt> GetNextPositions(Vector2DInt position)
    {
        return new Vector2DInt[] {
            position + Vector2DInt.Up,
            position + Vector2DInt.Down,
            position + Vector2DInt.Left,
            position + Vector2DInt.Right
        }
        .Where(pos => pos.X >= 0 && pos.Y >= 0)
        .Where(IsOpenSpace);
    }

    private bool IsOpenSpace(Vector2DInt position)
    {
        var x = position.X;
        var y = position.Y;
        var value = x * x + 3 * x + 2 * x * y + y + y * y + _favoriteNumber;
        int bitCount = System.Numerics.BitOperations.PopCount((uint)value);
        return bitCount % 2 == 0;
    }

    public int GetSeenCount()
    {
        return _seen.Count;
    }
}
