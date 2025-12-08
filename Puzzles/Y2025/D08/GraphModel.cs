namespace Artokai.AOC.Puzzles.Y2025.D08;

public class Graph
{
    public HashSet<Node> Nodes { get; } = new();
    public HashSet<Edge> Edges { get; } = new();
    public void Merge(List<Graph> allGraphs, Graph other)
    {
        if (other == this) return;
        allGraphs.Remove(other);
        foreach (var bNode in other.Nodes)
        {
            bNode.Graph = this;
        }

        Nodes.UnionWith(other.Nodes);
        Edges.UnionWith(other.Edges);
    }
}

public class Node
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }
    public required Graph Graph { get; set; }

    override public string ToString() => $"({X},{Y},{Z})";
}

public class Edge
{
    public required Node A { get; set; }
    public required Node B { get; set; }

    private long? _distanceSquared;

    public long DistanceSqueared
    {
        get
        {
            _distanceSquared ??= CalculateDistanceSquared();
            return _distanceSquared.Value;
        }
    }

    override public string ToString() => $"{A} <-> {B} : {DistanceSqueared}";

    private long CalculateDistanceSquared()
    {
        var dx = A.X - B.X;
        var dy = A.Y - B.Y;
        var dz = A.Z - B.Z;
        return dx * dx + dy * dy + dz * dz;
    }
}
