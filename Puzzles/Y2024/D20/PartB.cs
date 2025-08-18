using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D20;

[PuzzleInfo(year: 2024, day: 20, part: 2, title: "Race Condition")]
public class PartB : SolverBase
{
    private const int MIN_DISTANCE_TO_CHEAT = 100;
    private const int CHEAT_TIME = 20;

    public override string Solve()
    {
        var (map, start, end) = InputParser.ParseInput(Input);
        var (path, stepMap) = PathFinder.Find(map, start, end);

        var solutions = 0;
        foreach (var p in path.SkipLast(1))
        {
            var posSteps = stepMap[p];
            solutions += GetShortcutCountFromPosition(stepMap, p);
        }

        return solutions.ToString();

    }

    private bool IsShortcutGoodEnough(Dictionary<Vector2DInt, int> stepMap, Vector2DInt start, Vector2DInt end)
    {
        var startSteps = stepMap[start];
        var endSteps = stepMap.GetValueOrDefault(end, -1);
        if (endSteps < startSteps) { return false; }

        var normalDistance = endSteps - startSteps;
        var shortcutDistance = Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        var savedSteps = normalDistance - shortcutDistance;

        return savedSteps >= MIN_DISTANCE_TO_CHEAT;
    }

    private int GetShortcutCountFromPosition(Dictionary<Vector2DInt, int> stepMap, Vector2DInt position)
    {
        var maxDistance = CHEAT_TIME;
        var shortcutEnds = new HashSet<Vector2DInt>();
        for (int dy = -maxDistance; dy <= maxDistance; dy++)
        {
            for (int dx = -maxDistance; dx <= maxDistance; dx++)
            {
                if (Math.Abs(dx) + Math.Abs(dy) <= maxDistance)
                {
                    var end = new Vector2DInt(position.X + dx, position.Y + dy);
                    if (IsShortcutGoodEnough(stepMap, position, end))
                    {
                        shortcutEnds.Add(end);
                    }
                }
            }
        }
        return shortcutEnds.Count;
    }
}
