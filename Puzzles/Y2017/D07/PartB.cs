using System.Text.RegularExpressions;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D07;

[PuzzleInfo(year: 2017, day: 7, part: 2, title: "Recursive Circus")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var (root, allNodes) = ParseInput();
        var unbalancedParent = allNodes
            .Where(n => n.Children.Count > 0)
            .Where(n => n.Children
                .GroupBy(c => c.TotalWeight)
                .Count() > 1
            )
            .OrderByDescending(n => n.Level)
            .First();

        var targetWeight = unbalancedParent.Children
            .GroupBy(c => c.TotalWeight)
            .Select(g => new { TotalWeight = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .Select(g => g.TotalWeight)
            .First();

        var unbalancedChild = unbalancedParent.Children.Where(c => c.TotalWeight != targetWeight).First();
        var diff = targetWeight - unbalancedChild.TotalWeight;
        var correctedWeight = unbalancedChild.Weight + diff;

        return correctedWeight.ToString();
    }

    private (Node root, List<Node> allNodes) ParseInput()
    {
        // Read input to a dictionary of nodes
        var lines = Input.AsLines();
        var re = new Regex(@"^(?<name>\w+) \((?<weight>\d+)\)(?: -> (?<children>.+))?$");
        var nodeData = new Dictionary<string, (int weight, string[] children, bool isRoot)>();
        foreach (var line in lines)
        {
            var match = re.Match(line);
            if (!match.Success)
                throw new FormatException($"Line does not match expected format: {line}");

            var name = match.Groups["name"].Value;
            var weight = int.Parse(match.Groups["weight"].Value);
            var childIds = match.Groups["children"].Success
                ? match.Groups["children"].Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                : Array.Empty<string>();
            nodeData[name] = (weight, childIds, true);
        }

        // Find root and build list of node-objects
        var allNodes = new List<Node>();
        nodeData
            .SelectMany(entry => entry.Value.children)
            .ToList()
            .ForEach(child =>
            {
                nodeData[child] = (nodeData[child].weight, nodeData[child].children, false);
            });
        var rootData = nodeData.FirstOrDefault(kvp => kvp.Value.isRoot);
        var rootNode = new Node(0, rootData.Key, rootData.Value.weight);
        allNodes.Add(rootNode);
        allNodes.AddRange(PopulateChildren(rootNode, nodeData));
        return (rootNode, allNodes);
    }

    private List<Node> PopulateChildren(Node parent, Dictionary<string, (int weight, string[] children, bool isRoot)> nodeData)
    {
        var childNames = nodeData[parent.Name].children;
        var children = new List<Node>();
        foreach (var childName in childNames)
        {
            var childData = nodeData[childName];
            var childNode = new Node(parent.Level + 1, childName, childData.weight);
            children.Add(childNode);
            parent.Children.Add(childNode);
            children.AddRange(PopulateChildren(childNode, nodeData));
        }
        return children;
    }
}

public class Node
{
    public string Name { get; }
    public int Weight { get; }
    public List<Node> Children { get; }

    public int Level { get; }

    public Node(int level, string name, int weight)
    {
        Level = level;
        Name = name;
        Weight = weight;
        Children = new List<Node>();
    }

    public int TotalWeight => Weight + Children.Sum(c => c.TotalWeight);

    public override string ToString() => $"{Name} ({Weight})";
}