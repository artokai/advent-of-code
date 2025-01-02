using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D12;

[PuzzleInfo(year: 2024, day: 12, part: 2, title: "Garden Groups")]
public class PartB : SolverBase
{
    private char[,] _map = new char[0, 0];
    private int[,] _ids = new int[0, 0];
    private Dictionary<int, int> _areas = new();
    private Dictionary<int, char> _types = new();
    private Dictionary<int, int> _sides = new();

    public override string Solve()
    {
        _map = Input.AsCharTable();
        _ids = new int[_map.GetLength(0) + 2, _map.GetLength(1) + 2];
        _areas = new Dictionary<int, int>();
        _sides = new Dictionary<int, int>();
        _types = new Dictionary<int, char>();

        var id = 0;
        var perimeter = new Dictionary<int, int>();
        _areas = new Dictionary<int, int>();
        _types = new Dictionary<int, char>();
        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                if (_ids[x + 1, y + 1] == 0)
                {
                    id++;
                    var type = _map[x, y];
                    _areas[id] = 0;
                    _types[id] = type;
                    perimeter.Add(id, Flood(id, type, x, y));
                }
            }
        }

        for (var y = 1; y < _ids.GetLength(1) - 1; y++)
        {
            for (var x = 1; x < _ids.GetLength(0) - 1; x++)
            {
                id = _ids[x, y];
                if (!_sides.ContainsKey(id)) { _sides[id] = 0; }

                // left side
                if (_ids[x - 1, y] != id && (_ids[x, y - 1] != id || _ids[x - 1, y - 1] == id))
                {
                    _sides[id]++;
                }

                // right side
                if (_ids[x + 1, y] != id && (_ids[x, y - 1] != id || _ids[x + 1, y - 1] == id))
                {
                    _sides[id]++;
                }

                // top side
                if (_ids[x, y - 1] != id && (_ids[x - 1, y] != id || _ids[x - 1, y - 1] == id))
                {
                    _sides[id]++;
                }

                // bottom side
                if (_ids[x, y + 1] != id && (_ids[x - 1, y] != id || _ids[x - 1, y + 1] == id))
                {
                    _sides[id]++;
                }
            }
        }

        var price = 0L;
        foreach (var key in _areas.Keys)
        {
            price += _areas[key] * _sides[key];
        }

        return price.ToString();
    }

    private int Flood(int id, char type, int x, int y)
    {
        if (_ids[x + 1, y + 1] != 0) return 0;
        _ids[x + 1, y + 1] = id;
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
