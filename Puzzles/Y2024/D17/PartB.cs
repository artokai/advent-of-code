using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2024.D17;

[PuzzleInfo(year: 2024, day: 17, part: 2, title: "Chronospatial Computer")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var computer = new Computer();
        var target = new List<long> { 2, 4, 1, 5, 7, 5, 0, 3, 4, 0, 1, 6, 5, 5, 3, 0 };

        // Start searching from 0 on the last position
        var queue = new Queue<(long val, int idx)>([(val: 0, idx: 1)]);
        while (queue.Count != 0)
        {
            var (value, idx) = queue.Dequeue();
            for (var candidate = value; candidate <= value + 0b111; candidate++)
            {
                var result = computer.Run(candidate);
                var targetTail = target[^idx..];

                // Check if the output is equal to the last n elements of the target
                // If they are -> we might have found a match in this position,
                // Queue up the next position (idx+1) and start from zero
                if (result.Equals(string.Join(",", targetTail)))
                {
                    if (idx == target.Count)
                    {
                        return candidate.ToString();
                    }
                    queue.Enqueue((candidate << 3, idx + 1));
                }
            }
        }

        return "No solution found";
    }
}
