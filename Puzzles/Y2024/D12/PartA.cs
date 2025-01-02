using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D12;

[PuzzleInfo(year: 2024, day: 12, part: 1, title: "Garden Groups")]
public class PartA : SolverBase
{
    private char[,] _map = new char[0, 0];
    private int[,] _visited = new int[0, 0];
    private Dictionary<int, int> _areas = new();
    private Dictionary<int, char> _types = new();

    public override string Solve()
    {
        _map = Input.AsCharTable();
        _visited = new int[_map.GetLength(0), _map.GetLength(1)];
        _areas = new Dictionary<int, int>();
        _types = new Dictionary<int, char>();

        var id = 0;
        var perimeter = new Dictionary<int, int>();
        _areas = new Dictionary<int, int>();
        _types = new Dictionary<int, char>();
        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                if (_visited[x, y] == 0)
                {
                    id++;
                    var type = _map[x, y];
                    _areas[id] = 0;
                    _types[id] = type;
                    perimeter.Add(id, Flood(id, type, x, y));
                }
            }
        }

        var price = 0L;
        foreach (var key in _areas.Keys)
        {
            price += _areas[key] * perimeter[key];
        }

        return price.ToString();
    }

    private int Flood(int id, char type, int x, int y)
    {
        if (_visited[x, y] != 0) return 0;
        _visited[x, y] = id;
        _areas[id]++;

        var perimeter = 0;

        // LEFT
        var canFloodLeft = (x - 1 >= 0) && _map[x - 1, y] == type;
        perimeter += canFloodLeft ? Flood(id, type, x - 1, y) : 1;

        // RIGHT
        var canFloodRight = x + 1 < _map.GetLength(0) && _map[x + 1, y] == type;
        perimeter += canFloodRight ? Flood(id, type, x + 1, y) : 1;

        // UP
        if (y - 1 >= 0)
        {
            var canFloodUp = _map[x, y - 1] == type;
            perimeter += canFloodUp ? Flood(id, type, x, y - 1) : 1;
        }
        else
        {
            perimeter++;
        }

        // DOWN
        if (y + 1 < _map.GetLength(1))
        {
            var canFloodDown = _map[x, y + 1] == type;
            perimeter += canFloodDown ? Flood(id, type, x, y + 1) : 1;
        }
        else
        {
            perimeter++;
        }

        return perimeter;
    }
}
