using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D10;

[PuzzleInfo(year: 2025, day: 10, part: 1, title: "Factory")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var configs = Input.AsLines()
            .Select(line =>
            {
                var parts = line.Split(" ");
                var lights = parts[0][1..^1].Select(c => c == '#').ToList();
                var buttons = parts[1..^1].Select(btn => btn[1..^1].Split(',').Select(int.Parse).ToList()).ToList();
                // ignore the joltage for part A
                return (lights, buttons);
            });


        var result = configs
            .Select(config => SolveSingleMachine(config.lights, config.buttons))
            .Sum();
        return result.ToString();
    }

    private int SolveSingleMachine(List<bool> targetLights, List<List<int>> buttons)
    {
        var seen = new HashSet<string>();
        var q = new Queue<State>();
        q.Enqueue(new State(new bool[targetLights.Count], 0));
        while (q.Count > 0)
        {
            var state = q.Dequeue();
            var stateKey = string.Join(',', state.current.Select(b => b ? '1' : '0'));
            if (seen.Contains(stateKey)) continue;
            seen.Add(stateKey);

            if (state.current.SequenceEqual(targetLights))
            {
                return state.pressCount;
            }

            foreach (var button in buttons)
            {
                var newLights = (bool[])state.current.Clone();
                foreach (var index in button)
                {
                    newLights[index] = !newLights[index];
                }
                q.Enqueue(new State(newLights, state.pressCount + 1));
            }
        }

        throw new Exception("No solution found");
    }

    private record State(bool[] current, int pressCount);
}
