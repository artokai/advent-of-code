namespace Artokai.AOC.Puzzles.Y2016.D11;

public static class Solver
{
    public static int Solve(List<Floor> floors, int elementsCount)
    {
        var stateQueue = new Queue<State>();
        var seenStates = new HashSet<StateKey>();

        var initialState = new State(0, floors, 0, null);
        stateQueue.Enqueue(initialState);
        seenStates.Add(initialState.GetStateKey(elementsCount));

        // Breadth-first search (BFS) to find the shortest path to the target state
        while (stateQueue.Count > 0)
        {
            var currentState = stateQueue.Dequeue();
            var nextStates = currentState.GetNextStates(elementsCount);

            foreach (var nextState in nextStates)
            {
                var nextStateKey = nextState.GetStateKey(elementsCount);
                if (seenStates.Contains(nextStateKey))
                    continue;

                if (nextState.IsTargetState())
                    return nextState.steps;

                stateQueue.Enqueue(nextState);
                seenStates.Add(nextStateKey);
            }
        }

        // No solution found
        return int.MinValue;
    }
}
