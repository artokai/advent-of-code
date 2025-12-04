using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2025.D04;

public class RollScanner(char[,] map)
{
    private char GetTile(int x, int y) =>
        (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
            ? '~'
            : map[x, y];

    private bool IsFreeToMove(int x, int y)
    {
        var rolls = 0;
        rolls += GetTile(x - 1, y - 1) == '@' ? 1 : 0;
        rolls += GetTile(x, y - 1) == '@' ? 1 : 0;
        rolls += GetTile(x + 1, y - 1) == '@' ? 1 : 0;
        rolls += GetTile(x - 1, y) == '@' ? 1 : 0;
        rolls += GetTile(x + 1, y) == '@' ? 1 : 0;
        rolls += GetTile(x - 1, y + 1) == '@' ? 1 : 0;
        rolls += GetTile(x, y + 1) == '@' ? 1 : 0;
        rolls += GetTile(x + 1, y + 1) == '@' ? 1 : 0;
        return rolls < 4;
    }

    public List<Vector2DInt> Scan()
    {
        var rollsThatCanBeRemoved = new List<Vector2DInt>();
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                if (GetTile(x, y) == '@' && IsFreeToMove(x, y))
                {
                    rollsThatCanBeRemoved.Add(new Vector2DInt(x, y));
                }
            }
        }
        return rollsThatCanBeRemoved;
    }
}