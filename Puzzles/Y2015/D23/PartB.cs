using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D23;

[PuzzleInfo(year: 2015, day: 23, part: 2, title:"Opening the Turing Lock")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var emulator = new Emulator(Input.AsLines());
        emulator.SetRegisterValue("a", 1);
        emulator.Run();
        return emulator.GetRegisterValue("b").ToString();
    }
}
