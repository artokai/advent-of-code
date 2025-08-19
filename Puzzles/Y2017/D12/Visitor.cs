namespace Artokai.AOC.Puzzles.Y2017.D12;

public class Visitor
{
    public HashSet<int> Visited { get; private set; } = new();

    public void Visit(int current, Dictionary<int, List<int>> connections)
    {
        Visited.Add(current);
        foreach (var next in connections[current])
        {
            if (!HasVisited(next))
            {
                Visit(next, connections);
            }
        }
    }

    public bool HasVisited(int id)
    {
        return Visited.Contains(id);
    }
}
