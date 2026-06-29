namespace Artokai.AOC.Puzzles.Y2018.D09;

public static class Solver
{
    public static long Solve(int playerCount, int marbleCount)
    {
        var currentMarble = 0;
        var scores = new long[playerCount];
        var marbles = new LinkedList<int>();
        var current = marbles.AddFirst(0);

        while (currentMarble <= marbleCount)
        {
            currentMarble++;

            if (currentMarble % 23 == 0)
            {
                var scoreIndex = currentMarble % playerCount;
                scores[scoreIndex] += currentMarble;
                var nodeToRemove = GetMarble(marbles, current, -7);

                scores[scoreIndex] += nodeToRemove.Value;
                current = nodeToRemove.Next ?? marbles.First!;
                marbles.Remove(nodeToRemove);
            }
            else
            {
                var next = current.Next ?? marbles.First!;
                current = marbles.AddAfter(next, currentMarble);
            }
        }

        return scores.Max();
    }

    private static LinkedListNode<int> GetMarble(LinkedList<int> list, LinkedListNode<int> node, int delta)
    {
        Func<LinkedListNode<int>, LinkedListNode<int>> accessor = (delta >= 0)
            ? (LinkedListNode<int> c) => c.Next ?? list.First!
            : (LinkedListNode<int> c) => c.Previous ?? list.Last!;

        var current = node;
        var steps = Math.Abs(delta);
        for (var i = 0; i < steps; i++)
        {
            current = accessor(current);
        }
        return current;
    }
}
