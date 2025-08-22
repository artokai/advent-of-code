using System.ComponentModel;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D18;

[PuzzleInfo(year: 2017, day: 18, part: 2, title: "Duet")]
public class PartB : SolverBase
{
    DuetEmulator emulatorA = new();
    DuetEmulator emulatorB = new();
    Dictionary<DuetEmulator, Queue<long>> queues = new();
    Dictionary<DuetEmulator, bool> isWaiting = new();

    long sendCount = 0;

    public override string Solve()
    {
        var program = Input.AsLines();
        sendCount = 0;
        emulatorA = CreateEmulator(0, program);
        emulatorB = CreateEmulator(1, program);

        bool bothBlocked;
        do
        {
            emulatorA.Step();
            emulatorB.Step();
            bothBlocked = isWaiting[emulatorA] && isWaiting[emulatorB];
        } while (!bothBlocked && (emulatorA.Running || emulatorB.Running));

        return sendCount.ToString();
    }

    private DuetEmulator CreateEmulator(int id, List<string> program)
    {
        var emulator = new DuetEmulator(program);
        emulator.SetRegisterValue("p", id);
        emulator.OnRcv = HandleRcv;
        emulator.OnSnd = HandleSnd;

        queues[emulator] = new Queue<long>();
        isWaiting[emulator] = false;

        return emulator;
    }

    private void HandleSnd(DuetEmulator emulator, long value)
    {
        if (emulator == emulatorB) { sendCount++; }

        var other = emulator == emulatorA ? emulatorB : emulatorA;
        queues[other].Enqueue(value);
        emulator.Pointer++;
    }

    private void HandleRcv(DuetEmulator emulator, string register)
    {
        var queue = queues[emulator];
        if (queue.Count == 0)
        {
            // Do not increment pointer, we wait until we receive something
            isWaiting[emulator] = true;
            return;
        }

        isWaiting[emulator] = false;
        var received = queue.Dequeue();
        emulator.SetRegisterValue(register, received);
        emulator.Pointer++;
    }
}
