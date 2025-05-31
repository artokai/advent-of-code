using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D12;

[PuzzleInfo(year: 2016, day: 12, part: 2, title: "Leonardo's Monorail")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var emulator = new Emulator(Input);
        emulator.SetRegisterValue("c", 1);
        emulator.Run();
        return emulator.GetRegisterValue("a").ToString();
    }
}
