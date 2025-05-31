using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D12;

[PuzzleInfo(year: 2016, day: 12, part: 1, title: "Leonardo's Monorail")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var emulator = new Emulator(Input);
        emulator.Run();
        return emulator.GetRegisterValue("a").ToString();
    }
}
