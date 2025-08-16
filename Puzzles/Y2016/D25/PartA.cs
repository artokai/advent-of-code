using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D25;

[PuzzleInfo(year: 2016, day: 25, part: 1, title: "Clock Signal")]
public class PartA : SolverBase
{

    private const int MINIMUM_SEQUENCE_LENGTH = 15;

    public override string Solve()
    {
        var program = string.Join('\n', Input.AsLines());

        // Use optimizations from puzzle 2016/23 (not really necessary)
        var optimized = Optimizer.Optimize(program).Split('\n');

        var solved = false;
        var registerA = -1;
        while (!solved)
        {
            registerA++;
            solved = RunExperiment(registerA, optimized);
        }

        return registerA.ToString();
    }

    private bool RunExperiment(int registerA, string[] optimized)
    {
        var lastOut = -1;
        var currentSeqLength = 0;
        var solved = false;

        var handleOut = (Emulator emu, int value) =>
        {
            var isBinary = value == 0 || value == 1;
            if (!isBinary || value == lastOut)
            {
                emu.Stop();
                return;
            }

            currentSeqLength++;
            lastOut = value;

            if (currentSeqLength >= MINIMUM_SEQUENCE_LENGTH)
            {
                solved = true;
                emu.Stop();
            }
        };

        var emulator = new Emulator(optimized, handleOut);
        emulator.SetRegisterValue("a", registerA);
        emulator.Run();
        return solved;
    }
}
