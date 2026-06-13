using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D01;

[PuzzleInfo(year: 2018, day: 1, part: 2, title: "Chronal Calibration")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var changes = Input.AsLines()
            .Select(int.Parse)
            .ToArray();

        var seen = new HashSet<int>();
        var i = 0;
        var current = 0;
        while (!seen.Contains(current))
        {
            seen.Add(current);
            current += changes[i];
            i = i < changes.Length-1 ? i + 1 : 0;
        }

        return current.ToString();
    }
}
