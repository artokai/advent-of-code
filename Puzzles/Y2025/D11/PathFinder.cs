
namespace Artokai.AOC.Puzzles.Y2025.D11;

public static class PathFinder
{
    public static long DFS(Dictionary<string, Node> nodes, Node start, Node goal) =>
        DFS(
            nodes,
            start,
            goal,
            null,
            new Dictionary<string, long>(),
            new HashSet<string>()
        );

    public static long DFS(Dictionary<string, Node> nodes, Node start, Node goal, Node avoid) =>
        DFS(
            nodes,
            start,
            goal,
            avoid,
            new Dictionary<string, long>(),
            new HashSet<string>()
        );

    private static long DFS(Dictionary<string, Node> nodes, Node start, Node goal, Node? avoid, Dictionary<string, long> memo, HashSet<string> visited)
    {
        if (avoid != null && start == avoid)
        {
            return 0L;
        }

        if (start == goal)
        {
            return 1L;
        }

        if (memo.ContainsKey(start.Name))
        {
            return memo[start.Name];
        }

        var isLoop = visited.Contains(start.Name);
        if (isLoop)
        {
            return 0L;
        }
        visited.Add(start.Name);

        var pathsToGoal = 0L;
        foreach (var nextId in start.Outputs)
        {
            var next = nodes[nextId];
            var visitedCopy = new HashSet<string>(visited);
            pathsToGoal += DFS(nodes, next, goal, avoid, memo, visitedCopy);
        }
        memo[start.Name] = pathsToGoal;
        return pathsToGoal;
    }
}
