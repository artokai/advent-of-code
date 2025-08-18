using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D05;

[PuzzleInfo(year: 2017, day: 5, part: 1, title: "A Maze of Twisty Trampolines, All Alike")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var instructions = Input.AsListOf<int>();
        var pointer = 0;
        var steps = 0;
        while (pointer >= 0 && pointer < instructions.Count)
        {
            var offset = instructions[pointer];
            instructions[pointer] += 1;
            pointer += offset;
            steps++;
        }

        return steps.ToString();
    }
}
