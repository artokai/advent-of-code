using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D08;

[PuzzleInfo(year: 2015, day: 8, part: 2, title: "Matchsticks")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        return lines
            .Select(line => Escape(line).Length - line.Length)
            .Sum()
            .ToString();
    }

    private string Escape(string line) => "\"" + line.Replace(@"\", @"\\").Replace("\"", "\\\"") + "\"";
}
