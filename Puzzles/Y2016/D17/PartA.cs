using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D17;

[PuzzleInfo(year: 2016, day: 17, part: 1, title: "Two Steps Forward")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var passcode = Input.AsSingleLine().Trim();
        var initialHash = Helper.ComputeHash(passcode);
        var queue = new Queue<State>();
        queue.Enqueue(new State(Helper.InitialPosition, "", initialHash));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Position == Helper.TargetPosition)
            {
                return state.Path;
            }

            foreach (var next in Helper.GetNextStates(state, passcode))
            {
                queue.Enqueue(next);
            }
        }

        return "No solution found!";
    }
}
