namespace Artokai.AOC.Puzzles.Y2024.D18;

public record struct Coordinate(int X, int Y);

public class PathFinder
{
    public readonly int _mapSize;
    private int[,] _stepMap;
    private List<Coordinate> _obstacles;

    public PathFinder(List<Coordinate> obstacles, int mapSize)
    {
        _mapSize = mapSize;
        _obstacles = obstacles;
        _stepMap = CreateStepMap();
    }

    public int FindPath(Coordinate startPos, Coordinate endPos)
    {
        var iterations = 0L;
        var queue = new Queue<State>();
        queue.Enqueue(new State(startPos, 0));
        while (queue.Count > 0)
        {
            iterations++;

            var current = queue.Dequeue();
            if (_stepMap[current.Pos.X, current.Pos.Y] <= current.Steps)
            {
                continue;
            }
            _stepMap[current.Pos.X, current.Pos.Y] = current.Steps;

            if (current.Pos == endPos)
            {
                return current.Steps;
            }

            if (ShouldMoveTo(current.Up, current.Steps + 1))
            {
                queue.Enqueue(new State(current.Up, current.Steps + 1));
            }
            if (ShouldMoveTo(current.Right, current.Steps + 1))
            {
                queue.Enqueue(new State(current.Right, current.Steps + 1));
            }
            if (ShouldMoveTo(current.Down, current.Steps + 1))
            {
                queue.Enqueue(new State(current.Down, current.Steps + 1));
            }
            if (ShouldMoveTo(current.Left, current.Steps + 1))
            {
                queue.Enqueue(new State(current.Left, current.Steps + 1));
            }
        }
        return -1;
    }

    public void Reset(List<Coordinate> obstacles)
    {
        _obstacles = obstacles;
        _stepMap = CreateStepMap();
    }

    private bool ShouldMoveTo(Coordinate pos, int steps)
    {
        if (pos.X < 0 || pos.X >= _mapSize || pos.Y < 0 || pos.Y >= _mapSize)
        {
            return false;
        }

        if (_obstacles.Contains(pos))
        {
            return false;
        }

        if (_stepMap[pos.X, pos.Y] > steps)
        {
            return true;
        }

        return false;
    }

    private int[,] CreateStepMap(int initialValue = int.MaxValue)
    {
        var _stepMap = new int[_mapSize, _mapSize];
        for (int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                _stepMap[x, y] = initialValue;
            }
        }
        return _stepMap;
    }
}

public record struct State(Coordinate Pos, int Steps)
{
    public Coordinate Up => new Coordinate(Pos.X, Pos.Y - 1);
    public Coordinate Right => new Coordinate(Pos.X + 1, Pos.Y);
    public Coordinate Down => new Coordinate(Pos.X, Pos.Y + 1);
    public Coordinate Left => new Coordinate(Pos.X - 1, Pos.Y);
}