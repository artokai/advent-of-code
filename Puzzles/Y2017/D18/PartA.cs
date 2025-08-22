using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D18;

[PuzzleInfo(year: 2017, day: 18, part: 1, title: "Duet")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var program = Input.AsLines();
        var lastPlayedSound = long.MinValue;

        var emulator = new DuetEmulator(program);
        emulator.OnSnd = (_, value) =>
        {
            lastPlayedSound = value;
            emulator.Pointer++;
        };

        emulator.OnRcv = (_, valueOrRegister) =>
        {
            var value = emulator.GetValueOrRegister(valueOrRegister);
            if (value == 0)
            {
                emulator.Pointer++;
            }
            else
            {
                emulator.Stop();
            }
        };
        emulator.Run();
        return lastPlayedSound.ToString();
    }
}
