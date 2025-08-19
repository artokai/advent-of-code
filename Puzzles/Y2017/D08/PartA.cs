using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D08;

[PuzzleInfo(year: 2017, day: 8, part: 1, title: "I Heard You Like Registers")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var emulator = new Emulator(lines);
        emulator.Run();
        var maxValue = emulator.Registers.Values.Max();
        return maxValue.ToString();
    }
}
