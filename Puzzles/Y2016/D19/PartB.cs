using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D19;

[PuzzleInfo(year: 2016, day: 19, part: 2, title: "An Elephant Named Joseph")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var count = int.Parse(Input.AsSingleLine());
        var startHalf = new Queue<int>(Enumerable.Range(1, count / 2));
        var endHalf = new Queue<int>(Enumerable.Range(count / 2 + 1, (count + 1) / 2));

        while (startHalf.Count + endHalf.Count > 1)
        {
            var currentElf = startHalf.Dequeue();
            endHalf.Dequeue();
            endHalf.Enqueue(currentElf);
            BalanceHalves(startHalf, endHalf);
        }

        var winner = startHalf.Dequeue();
        return winner.ToString();
    }

    private void BalanceHalves(Queue<int> start, Queue<int> end)
    {
        while (start.Count == 0 || end.Count > start.Count + 1)
        {
            var elf = end.Dequeue();
            start.Enqueue(elf);
        }
    }
}
