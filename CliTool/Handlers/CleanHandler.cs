
namespace Artokai.AOC.CliTool.Handlers;

public class CleanHandler
{
    public void Invoke()
    {
        var puzzleDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Puzzles");
        var yearDirectories = Directory.GetDirectories(puzzleDirectoryPath);
        var puzzleDirectories = yearDirectories
            .Select(Directory.GetDirectories)
            .SelectMany(puzzleDirectories => puzzleDirectories)
            .ToList();
        foreach (var puzzleDirectory in puzzleDirectories)
        {
            CleanPuzzle(puzzleDirectory);
        }
    }

    private void CleanPuzzle(string puzzleDirectory)
    {
        var dirnamesToRemove = new[] { "bin", "obj", "publish", ".vscode", ".vs" };
        var directoriesToRemove = Directory.GetDirectories(puzzleDirectory)
            .Where(d =>
            {
                var directoryName = Path.GetFileName(d);
                return dirnamesToRemove.Contains(directoryName, StringComparer.OrdinalIgnoreCase);
            })
            .ToList();

        foreach (var directory in directoriesToRemove)
        {
            Console.WriteLine($"Removing {directory}");
            Directory.Delete(directory, true);
        }
    }
}
