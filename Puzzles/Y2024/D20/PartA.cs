using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D20;

[PuzzleInfo(year: 2024, day: 20, part: 1, title: "Race Condition")]
public class PartA : SolverBase
{
    private const int MIN_DISTANCE_TO_CHEAT = 100;

    public override string Solve()
    {
        var (map, start, end) = InputParser.ParseInput(Input);

        var (path, stepMap) = PathFinder.Find(map, start, end);
        List<(Vector2DInt, Vector2DInt)> shortcuts = new List<(Vector2DInt, Vector2DInt)>();
        var IsShortcut = (Vector2DInt start, Vector2DInt end) =>
        {
            var startSteps = stepMap[start];
            var endSteps = stepMap.GetValueOrDefault(end, -1);
            if (endSteps > startSteps + MIN_DISTANCE_TO_CHEAT)
            {
                var shortcutStart = (start.Y == end.Y)
                    ? new Vector2DInt(Math.Min(start.X, end.X) + 1, start.Y)
                    : new Vector2DInt(start.X, Math.Min(start.Y, end.Y) + 1);
                shortcuts.Add((shortcutStart, end));
                return true;
            }
            return false;
        };

        var solutions = 0;
        foreach (var p in path.SkipLast(1))
        {
            var posSteps = stepMap[p];
            solutions += IsShortcut(p, new Vector2DInt(p.X, p.Y - 2)) ? 1 : 0;
            solutions += IsShortcut(p, new Vector2DInt(p.X + 2, p.Y)) ? 1 : 0;
            solutions += IsShortcut(p, new Vector2DInt(p.X, p.Y + 2)) ? 1 : 0;
            solutions += IsShortcut(p, new Vector2DInt(p.X - 2, p.Y)) ? 1 : 0;
        }

        return solutions.ToString();
    }
}
