using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D25;

[PuzzleInfo(year: 2017, day: 25, part: 1, title: "The Halting Problem")]
public class PartA : SolverBase
{
    private HashSet<int> tape = new();

    public override string Solve()
    {
        var sections = Input.SplitOnEmptyLines();
        var header = sections[0].AsLines();
        var initialState = header[0].Split(' ')[3][0];
        var steps = int.Parse(header[1].Split(' ')[5]);

        var states = new Dictionary<char, State>();
        for (var i = 1; i < sections.Count; i++)
        {
            var lines = sections[i].AsLines().Select(line => line.Trim()).ToList();
            var name = lines[0].Split(' ')[2][0];
            var writeIfZero = int.Parse(lines[2].Split(' ')[4].TrimEnd('.'));
            var moveIfZero = lines[3].Split(' ')[6] == "right." ? 1 : -1;
            var nextIfZero = lines[4].Split(' ')[4][0];
            var writeIfOne = int.Parse(lines[6].Split(' ')[4].TrimEnd('.'));
            var moveIfOne = lines[7].Split(' ')[6] == "right." ? 1 : -1;
            var nextIfOne = lines[8].Split(' ')[4][0];
            states[name] = new State(name, writeIfZero, moveIfZero, nextIfZero, writeIfOne, moveIfOne, nextIfOne);
        }

        var position = 0;
        var currentState = states[initialState];
        for (var i = 0; i < steps; i++)
        {
            var currentValue = GetValue(position);
            if (currentValue == 0)
            {
                Write(position, currentState.WriteIfZero);
                position += currentState.MoveIfZero;
                currentState = states[currentState.StateIfZero];
            }
            else
            {
                Write(position, currentState.WriteIfOne);
                position += currentState.MoveIfOne;
                currentState = states[currentState.StateIfOne];
            }
        }

        return tape.Count.ToString();
    }

    private int GetValue(int position)
    {
        return tape.Contains(position) ? 1 : 0;
    }

    private void Write(int position, int value)
    {
        if (value == 1)
            tape.Add(position);
        else
            tape.Remove(position);
    }
}


public record State(
    char Name,
    int WriteIfZero,
    int MoveIfZero,
    char StateIfZero,
    int WriteIfOne,
    int MoveIfOne,
    char StateIfOne
);
