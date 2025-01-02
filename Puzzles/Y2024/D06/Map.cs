using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D06;

public class Map
{
    private bool[] _map;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Map(bool[] map, int width, int height)
    {
        _map = map;
        Width = width;
        Height = height;
    }

    public bool Get(Vector2DInt pos) => Get(pos.X, pos.Y);
    public bool Get(int x, int y) => IsOutside(x, y) ? false : _map[y * Width + x];

    public void Set(Vector2DInt pos, bool blocked) => Set(pos.X, pos.Y, blocked);
    public void Set(int x, int y, bool blocked)
    {
        if (IsOutside(x, y)) { return; }
        _map[y * Width + x] = blocked;
    }

    public bool IsOutside(Vector2DInt pos) => IsOutside(pos.X, pos.Y);
    public bool IsOutside(int x, int y)
    {
        return x < 0 || x >= Width || y < 0 || y >= Height;
    }

    public Map Clone()
    {
        bool[] newMap = (bool[])_map.Clone();
        return new Map(newMap, Width, Height);
    }
}
