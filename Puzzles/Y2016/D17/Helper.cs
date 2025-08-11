using System.Security.Cryptography;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D17;

public static class Helper
{
    public static readonly int MaxX = 3;
    public static readonly int MaxY = 3;

    public static readonly Vector2DInt InitialPosition = new Vector2DInt(0, 0);

    public static readonly Vector2DInt TargetPosition = new Vector2DInt(MaxX, MaxY);

    private static readonly Dictionary<string, Vector2DInt> Directions = new Dictionary<string, Vector2DInt>
    {
        { "U", Vector2DInt.Up },
        { "D", Vector2DInt.Down },
        { "L", Vector2DInt.Left },
        { "R", Vector2DInt.Right }
    };

    public static IEnumerable<State> GetNextStates(State current, string passcode)
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
                yield return new State(nextPos, nextPath, nextHash);;
            }
    }

    public static string ComputeHash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}

public record State(Vector2DInt Position, string Path, string Hash);
