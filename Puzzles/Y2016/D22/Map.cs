using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D22;

public record class Map
{
    public int Width { get; }
    public int Height { get; }
    public bool[,] Tiles { get; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new bool[width, height];
    }

    public void SetTile(int x, int y, bool isWall)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException($"Coordinates ({x}, {y}) are out of bounds.");
        }
        Tiles[x, y] = isWall;
    }

    public void Print(Vector2DInt location)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (x == location.X && y == location.Y)
                {
                    Console.Write("E");
                    continue;
                }
                Console.Write(Tiles[x, y] ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}
