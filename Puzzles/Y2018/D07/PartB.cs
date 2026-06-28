using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2018.D07;

[PuzzleInfo(year: 2018, day: 7, part: 2, title: "The Sum of Its Parts")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var maxThreads = 5;
        var waiting = InputParser.Parse(Input);
        var processed = new List<Node>();
        var taskCount = waiting.Count;

        var clock = 0;
        var threads = new List<Thread>();
        while (processed.Count < taskCount)
        {
            // Mark threads finishing at current timestamp done
            var completedThreads = threads
                .Where(t => t.StartTime + t.Node.Duration == clock)
                .OrderBy(t => t.Node.Key);
            foreach (var t in completedThreads)
            {
                threads.Remove(t);
                processed.Add(t.Node);
                foreach (var dependent in t.Node.Dependents)
                {
                    dependent.Predecessors.Remove(t.Node);
                }
            }

            // Try queue new tasks (if any are free)
            var nextTask = GetNextTask(waiting);
            while (nextTask != null && threads.Count < maxThreads)
            {
                threads.Add(new Thread(clock, nextTask));
                waiting.Remove(nextTask);
                nextTask = GetNextTask(waiting);
            }

            // Advance clock to the next finishing thread endtime
            if (threads.Count > 0)
            {
                clock = threads.Min(t => t.StartTime + t.Node.Duration);
            }
        }

        return clock.ToString();
    }

    private Node? GetNextTask(List<Node> waiting)
    {
        return waiting.Where(n => n.Predecessors.Count <= 0)
                .OrderBy(n => n.Key)
                .FirstOrDefault();
    }
}

public record Thread(int StartTime, Node Node);