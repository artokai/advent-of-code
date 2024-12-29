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

    public List<string> AsLines()
    {
        if (_lines == null)
        {
            if (!File.Exists(InputPath)) {
                throw new FileNotFoundException($"Input file not found: {InputPath}");
            }
            _lines = File.ReadAllLines(InputPath).ToList();
        }
        return _lines;
    }

    public string AsSingleLine() => string.Join("", AsLines());
}