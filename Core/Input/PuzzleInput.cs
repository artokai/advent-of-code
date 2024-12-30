using System.Globalization;

namespace Artokai.AOC.Core.Input;

public class PuzzleInput
{
    public string InputPath { get; init; }
    private List<string>? _lines = null;

    public PuzzleInput(string inputPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(inputPath, nameof(inputPath));
        InputPath = inputPath;
    }

    public PuzzleInput(string inputPath, List<string> lines)
    {
        ArgumentException.ThrowIfNullOrEmpty(inputPath, nameof(inputPath));
        InputPath = inputPath;
        _lines = lines;
    }

    public List<string> AsLines()
    {
        if (_lines == null)
        {
            if (!File.Exists(InputPath))
            {
                throw new FileNotFoundException($"Input file not found: {InputPath}");
            }
            _lines = File.ReadAllLines(InputPath).ToList();
        }
        return _lines;
    }

    public string AsSingleLine() => string.Join("", AsLines());

    public IEnumerable<(T1, T2)> AsPairs<T1, T2>(char[]? separators = null, IFormatProvider? formatProvider = null)
        where T1 : IParsable<T1>
        where T2 : IParsable<T2>
    {
        separators ??= [' ', '\t', ',', ';'];
        formatProvider ??= CultureInfo.InvariantCulture;
        var lines = AsLines();
        return lines.Select(line =>
        {
            var parts = line.Split(separators, 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return (T1.Parse(parts[0], formatProvider), T2.Parse(parts[1], formatProvider));
        });
    }

    public List<List<T>> AsColumns<T>(char[]? separators = null, IFormatProvider? formatProvider = null)
        where T : IParsable<T>
    {
        separators ??= [' ', '\t', ',', ';'];
        formatProvider ??= CultureInfo.InvariantCulture;
        var columns = new List<List<T>>();
        var lines = AsLines();
        foreach (var line in lines)
        {
            var parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                if (columns.Count <= i)
                {
                    columns.Add(new List<T>());
                }
                columns[i].Add(T.Parse(parts[i], formatProvider));
            }
        }
        return columns;
    }

    public List<List<T>> AsLists<T>(char[]? separators = null, IFormatProvider? formatProvider = null)
        where T : IParsable<T>
    {
        separators ??= [' ', '\t', ',', ';'];
        formatProvider ??= CultureInfo.InvariantCulture;
        var lists = new List<List<T>>();
        var lines = AsLines();
        foreach (var line in lines)
        {
            var parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            lists.Add(parts.Select(part => T.Parse(part, formatProvider)).ToList());
        }
        return lists;
    }

    public char[,] AsCharTable()
    {
        var lines = AsLines();
        var table = new char[lines[0].Length, lines.Count];
        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                table[x, y] = lines[y][x];
            }
        }
        return table;
    }

    public List<PuzzleInput> SplitOnEmptyLines()
    {
        var sourceLines = AsLines();
        var splitInput = new List<PuzzleInput>();
        var currentInput = new List<string>();
        foreach (var line in sourceLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                splitInput.Add(new PuzzleInput(InputPath, currentInput));
                currentInput = new List<string>();
            }
            else
            {
                currentInput.Add(line);
            }
        }
        if (currentInput.Count > 0)
        {
            splitInput.Add(new PuzzleInput(InputPath, currentInput));
        }
        return splitInput;
    }
}
