
namespace Artokai.AOC.Puzzles.Y2015.D18;

public class Simulation
{
    private char[,] _map;
    private int _width;
    private int _height;

    public Simulation(char[,] map)
    {
        _map = map;
        _width = _map.GetLength(0);
        _height = _map.GetLength(1);
    }

    public void Update()
    {
        var newMap = new char[_width, _height];
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var active = GetActiveNeigbourCount(x, y);
                if (_map[x, y] == '#')
                {
                    newMap[x, y] = (active != 2 && active != 3) ? '.' : '#';
                }
                else
                {
                    newMap[x, y] = (active == 3) ? '#' : '.';
                }
            }
        }
        _map = newMap;
    }

    private int GetActiveNeigbourCount(int x, int y)
    {
        var cnt = 0;
        if (IsActive(x - 1, y - 1)) cnt++;
        if (IsActive(x - 1, y)) cnt++;
        if (IsActive(x - 1, y + 1)) cnt++;
        if (IsActive(x, y - 1)) cnt++;
        if (IsActive(x, y + 1)) cnt++;
        if (IsActive(x + 1, y - 1)) cnt++;
        if (IsActive(x + 1, y)) cnt++;
        if (IsActive(x + 1, y + 1)) cnt++;
        return cnt;
    }

    private bool IsActive(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return false;
        }
        return _map[x, y] == '#';
    }

    public int GetActiveCount()
    {
        var cnt = 0;
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_map[x, y] == '#')
                {
                    cnt++;
                }
            }
        }
        return cnt;
    }

    public void Set(int x, int y, char value)
    {
        _map[x, y] = value;
    }
}