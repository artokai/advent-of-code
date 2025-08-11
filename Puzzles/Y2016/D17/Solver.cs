using System.Security.Cryptography;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D17;

public static class Solver
{
    private static readonly int MaxX = 3;
    private static readonly int MaxY = 3;
    private static readonly Dictionary<string, Vector2DInt> Directions = new()
    {
        { "U", Vector2DInt.Up },
        { "D", Vector2DInt.Down },
        { "L", Vector2DInt.Left },
        { "R", Vector2DInt.Right }
    };

    public static IEnumerable<string> Solve(string passcode)
    {
        var initialPosition = new Vector2DInt(0, 0);
        var targetPosition = new Vector2DInt(MaxX, MaxY);
        var initialHash = ComputeHash(passcode);

        var queue = new Queue<State>();
        queue.Enqueue(new State(initialPosition, "", initialHash));
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Position == targetPosition)
            {
                yield return state.Path;
            }
            else
            {
                var nextStates = GetNextStates(state, passcode);
                queue.EnqueueRange(nextStates);
            }
        }
    }

    private static IEnumerable<State> GetNextStates(State current, string passcode)
    {

        foreach (var dir in Directions)
        {
            var nextPos = current.Position + dir.Value;
            if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X > MaxX || nextPos.Y > MaxY)
                continue;

            var key = dir.Key switch
            {
                "U" => current.Hash[0],
                "D" => current.Hash[1],
                "L" => current.Hash[2],
                "R" => current.Hash[3],
                _ => throw new ArgumentOutOfRangeException()
            };

            if (key < 'b' || key > 'f')
                continue;

            var nextPath = current.Path + dir.Key;
            var nextHash = ComputeHash(passcode + nextPath);
            yield return new State(nextPos, nextPath, nextHash); ;
        }
    }

    private static string ComputeHash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    private static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
    {
        foreach (var item in items) {
           queue.Enqueue(item);
        }
    }    
}

public record State(Vector2DInt Position, string Path, string Hash);
