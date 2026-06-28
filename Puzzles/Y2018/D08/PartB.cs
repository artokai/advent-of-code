using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D08;

[PuzzleInfo(year: 2018, day: 8, part: 2, title: "Memory Maneuver")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var numbers = Input.AsSingleLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();
        var q = new Queue<int>(numbers);

        var result = ParseNode(q);
        return result.ToString();
    }

    private int ParseNode(Queue<int> q)
    {
        var childCount = q.Dequeue();
        var metadataCount = q.Dequeue();

        // No children -> Return sum of metadata nodes
        if (childCount <= 0)
        {
            var sum = 0;
            for (var i = 0; i < metadataCount; i++)
            {
                var meta = q.Dequeue();
                sum += meta;
            }
            return sum;
        }

        // Calculate values of each child
        var childValues = new List<int>();
        for (var i = 0; i < childCount; i++)
        {
            childValues.Add(ParseNode(q));
        }

        // Sum the values of children referenced by metadata fields
        var childSum = 0;
        for (var i = 0; i < metadataCount; i++)
        {
            var childIndex = q.Dequeue() - 1;
            if (childIndex < 0) { continue; }
            if (childIndex >= childValues.Count) { continue; }
            childSum += childValues[childIndex];
        }
        return childSum;
    }
}
