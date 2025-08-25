using System.Text;

namespace Artokai.AOC.Puzzles.Y2017.D21;

public class Grid(bool[,] Cells, int Size)
{
    public bool[,] Cells { get; } = Cells;
    public int Size { get; } = Size;

    public static Grid Parse(string input)
    {
        var rows = input.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var size = rows.Length;
        var cells = new bool[size, size];
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                cells[x, y] = rows[y][x] == '#';
            }
        }

        return new Grid(cells, size);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                sb.Append(Cells[x, y] ? '#' : '.');
            }
            if (y < Size - 1)
                sb.Append('/');
        }
        return sb.ToString();
    }

    public int CountActiveCells()
    {
        var cnt = 0;
        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                if (Cells[x, y])
                {
                    cnt++;
                }
            }
        }
        return cnt;
    }

    public Grid RotateLeft()
    {
        var cells = new bool[Size, Size];
        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                cells[y, Size - 1 - x] = Cells[x, y];
            }
        }
        return new Grid(cells, Size);
    }

    public Grid FlipHorizontal()
    {
        var cells = new bool[Size, Size];
        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                cells[Size - 1 - x, y] = Cells[x, y];
            }
        }
        return new Grid(cells, Size);
    }

    public Grid ExtractSubGrid(int x, int y, int patternSize)
    {
        var cells = new bool[patternSize, patternSize];
        for (var dy = 0; dy < patternSize; dy++)
        {
            for (var dx = 0; dx < patternSize; dx++)
            {
                cells[dx, dy] = Cells[x + dx, y + dy];
            }
        }
        return new Grid(cells, patternSize);
    }
}
