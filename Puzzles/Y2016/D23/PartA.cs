using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D23;

[PuzzleInfo(year: 2016, day: 23, part: 1, title: "Safe Cracking")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var emulator = new Emulator(Input);
        emulator.SetRegisterValue("a", 7); 
        emulator.Run();
        return emulator.GetRegisterValue("a").ToString();
    }
}
