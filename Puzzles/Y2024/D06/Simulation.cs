
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D06;

public struct Simulation
{
    private Map _map;
    private Guard _guard;
    public Vector2DInt? Obstacle;

    public bool TrackEntries { get; set; } = true;

    public int Iterations { get; private set; }
    public byte[] Visited { get; private set; }
    public byte[] Entries { get; private set; }

    public bool IsRunningInCircles { get; private set; }

    public Simulation(Map map, Guard guard)
    {
        _map = map;
        _guard = guard;
        Obstacle = null;
        Iterations = 0;
        IsRunningInCircles = false;
        Visited = new byte[map.Width * map.Height];
        Entries = new byte[map.Width * map.Height];
    }

    public Simulation(Map map, Vector2DInt obstaclePos, byte oldDir)
    {
        var guardPos = new Vector2DInt(obstaclePos.X, obstaclePos.Y + 1);
        var guardDir = Guard.B_UP;

        if (oldDir == Guard.B_DOWN)
        {
            guardPos = new Vector2DInt(obstaclePos.X, obstaclePos.Y - 1);
            guardDir = Guard.B_DOWN;
        }
        else if (oldDir == Guard.B_LEFT)
        {
            guardPos = new Vector2DInt(obstaclePos.X + 1, obstaclePos.Y);
            guardDir = Guard.B_LEFT;
        }
        else if (oldDir == Guard.B_RIGHT)
        {
            guardPos = new Vector2DInt(obstaclePos.X - 1, obstaclePos.Y);
            guardDir = Guard.B_RIGHT;
        }

        _map = map;
        _guard = new Guard(guardPos, guardDir);
        Obstacle = obstaclePos;
        Iterations = 0;
        IsRunningInCircles = false;
        Visited = new byte[map.Width * map.Height];
        Entries = new byte[map.Width * map.Height];
    }

    public bool Run()
    {
        if (_guard.Position.Y < 0)
        {
            Console.WriteLine("Guard is outside the map");
        }

        while (_guard.IsMoving)
        {
            Iterations++;

            var enterDirection = _guard.DirectionByte;
            var nextPos = _guard.Position + _guard.DirectionVector;
            bool blocked = Obstacle == nextPos || _map.Get(nextPos);
            if (blocked)
            {
                if ((Visited[_guard.Position.Y * _map.Width + _guard.Position.X] & _guard.DirectionByte) != 0)
                {
                    IsRunningInCircles = true;
                    _guard.Stop();
                    break;
                }

                while (blocked)
                {
                    _guard.TurnRight();
                    nextPos = _guard.Position + _guard.DirectionVector;
                    blocked = Obstacle == nextPos || _map.Get(nextPos);
                }
            }
            Visited[_guard.Position.Y * _map.Width + _guard.Position.X] |= enterDirection;


            // Move
            _guard.Position = nextPos;
            if (_map.IsOutside(_guard.Position))
            {
                _guard.Stop();
                break;
            }

            if (TrackEntries)
            {
                var i = _guard.Position.Y * _map.Width + _guard.Position.X;
                if (Entries[i] == 0) { Entries[i] = _guard.DirectionByte; }
            }
        }
        return _map.IsOutside(_guard.Position);
    }

    public IEnumerable<Vector2DInt> GetVisitedPositions()
    {
        for (int i = 0; i < Visited.Length; i++)
        {
            if (Visited[i] != 0)
            {
                var x = i % _map.Width;
                var y = i / _map.Width;
                yield return new Vector2DInt(x, y);
            }
        }
    }

    public IEnumerable<Vector2DInt> GetEntries()
    {
        for (int i = 0; i < Entries.Length; i++)
        {
            if (Entries[i] != 0)
            {
                var x = i % _map.Width;
                var y = i / _map.Width;
                yield return new Vector2DInt(x, y);
            }
        }
    }

    private void PrintMap()
    {
        Console.WriteLine("");
        Console.WriteLine("Iteration: " + Iterations);
        for (int y = 0; y < _map.Height; y++)
        {
            Console.Write($"{y:D3}");
            for (int x = 0; x < _map.Width; x++)
            {
                var pos = new Vector2DInt(x, y);
                if (pos == _guard.Position)
                {
                    Console.Write(_guard.GetDirectionChar());
                }
                else if (Obstacle != null && pos == Obstacle)
                {
                    Console.Write('O');
                }
                else if (_map.Get(pos))
                {
                    Console.Write('#');
                }
                else if (Visited[y * _map.Width + x] != 0)
                {
                    Console.Write('+');
                }
                else
                {
                    Console.Write(' ');
                }

            }
            Console.WriteLine("");
        }
    }

}
