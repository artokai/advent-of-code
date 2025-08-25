using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D23;

[PuzzleInfo(year: 2017, day: 23, part: 1, title: "Coprocessor Conflagration")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var emulator = new Emulator(Input.AsLines());
        emulator.Run();
        return emulator.MulCount.ToString();
    }
}
