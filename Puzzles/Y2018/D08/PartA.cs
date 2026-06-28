using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D08;

[PuzzleInfo(year: 2018, day: 8, part: 1, title: "Memory Maneuver")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var numbers = Input.AsSingleLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s => int.Parse(s))
            .ToList();
        var q = new Queue<int>(numbers);

        var sum = ParseNode(q);
        return sum.ToString();    
    }

    private int ParseNode(Queue<int> q)
    {
        var sum = 0;
        var childCount = q.Dequeue();
        var metadataCount = q.Dequeue();
        for (var i = 0; i < childCount; i++)
        {
            sum += ParseNode(q);
        }
        for (var i = 0; i < metadataCount; i++)
        {
            var meta = q.Dequeue();
            sum += meta;
        }
        return sum;
    }
}
