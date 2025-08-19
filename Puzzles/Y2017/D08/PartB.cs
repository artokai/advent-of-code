using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D08;

[PuzzleInfo(year: 2017, day: 8, part: 2, title: "I Heard You Like Registers")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var lines = Input.AsLines();
        var emulator = new Emulator(lines);
        emulator.Run();
        var maxSeenValue = emulator.MaxSeenValue.HasValue
            ? emulator.MaxSeenValue.Value.RegisterValue
            : int.MinValue;
        return maxSeenValue.ToString();
    }
}
