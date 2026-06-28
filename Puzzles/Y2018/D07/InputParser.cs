using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2018.D07;

public static class InputParser
{
    public static List<Node> Parse(PuzzleInput input)
    {
        var nodeDictionary = new Dictionary<char, Node>();
        input.AsLines()
            .Select(line => (line[5], line[36]))
            .ToList()
            .ForEach(pair =>
            {
                if (!nodeDictionary.ContainsKey(pair.Item1))
                    nodeDictionary.Add(pair.Item1, new Node(pair.Item1, GetDuration(pair.Item1), [], []));
                if (!nodeDictionary.ContainsKey(pair.Item2))
                    nodeDictionary.Add(pair.Item2, new Node(pair.Item2, GetDuration(pair.Item2), [], []));

                var predecessor = nodeDictionary[pair.Item1];
                var dependent = nodeDictionary[pair.Item2];
                predecessor.Dependents.Add(dependent);
                dependent.Predecessors.Add(predecessor);
            });
        return nodeDictionary.Values.ToList();
    }

    private static int GetDuration(char task)
    {
        return 60 + (task - 'A') + 1;
    }
}

public record Node(
    char Key,
    int Duration,
    HashSet<Node> Predecessors,
    HashSet<Node> Dependents
);
