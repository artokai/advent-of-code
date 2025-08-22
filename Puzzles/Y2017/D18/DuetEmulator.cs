
namespace Artokai.AOC.Puzzles.Y2017.D18;

public class DuetEmulator
{
    public int Pointer { get; set; } = 0;
    private List<Action> instructions;
    private Dictionary<string, long> registers = new();
    public bool Running { get; set; }

    public Action<DuetEmulator, string> OnRcv { get; set; }
    public Action<DuetEmulator, long> OnSnd { get; set; }

    public DuetEmulator() : this(new List<string>()) { }

    public DuetEmulator(List<string> program)
    {
        Running = true;
        Pointer = 0;
        registers.Clear();
        OnRcv = (emulator, value) => { throw new NotImplementedException("Rcv not implemented."); };
        OnSnd = (emulator, value) => { throw new NotImplementedException("Snd not implemented."); };
        instructions = program.Select(ParseInstruction).ToList();
    }

    private Action ParseInstruction(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var cmd = parts[0];
        var args = parts.Skip(1).ToArray();
        return cmd switch
        {
            "snd" => SndCommand(args),
            "set" => SetCommand(args),
            "add" => AddCommand(args),
            "mul" => MulCommand(args),
            "mod" => ModCommand(args),
            "rcv" => RcvCommand(args),
            "jgz" => JgzCommand(args),
            _ => throw new ArgumentException($"Invalid instruction: {line}"),
        };
    }

    public void Run()
    {
        Pointer = 0;
        while (Running)
        {
            Step();
        }
    }

    public void Step()
    {
        if (!Running || Pointer < 0 || Pointer >= instructions.Count)
        {
            Running = false;
            return;
        }
        var currentInstruction = instructions[Pointer];
        currentInstruction();
        Running = Running && Pointer >= 0 && Pointer < instructions.Count;
    }

    public void Stop()
    {
        Running = false;
    }

    public long GetValueOrRegister(string valueOrRegister)
    {
        if (long.TryParse(valueOrRegister, out var value))
            return value;
        return registers.GetValueOrDefault(valueOrRegister, 0);
    }

    public void SetRegisterValue(string register, long value)
    {
        registers[register] = value;
    }

    private Action SndCommand(string[] args) => () =>
    {
        OnSnd(this, GetValueOrRegister(args[0]));
    };

    private Action SetCommand(string[] args) => () =>
    {
        var y = GetValueOrRegister(args[1]);
        registers[args[0]] = y;
        Pointer++;
    };

    private Action AddCommand(string[] args) => () =>
    {
        var y = GetValueOrRegister(args[1]);
        registers[args[0]] += y;
        Pointer++;
    };

    private Action MulCommand(string[] args) => () =>
    {
        var x = GetValueOrRegister(args[0]);
        var y = GetValueOrRegister(args[1]);
        registers[args[0]] = x * y;
        Pointer++;
    };

    private Action ModCommand(string[] args) => () =>
    {
        var x = GetValueOrRegister(args[0]);
        var y = GetValueOrRegister(args[1]);
        registers[args[0]] = x % y;
        Pointer++;
    };

    private Action RcvCommand(string[] args) => () =>
    {
        var oldPntr = Pointer;
        OnRcv(this, args[0]);
        //Waiting = Pointer == oldPntr;
    };

    private Action JgzCommand(string[] args) => () =>
    {
        var x = GetValueOrRegister(args[0]);
        var y = (int)GetValueOrRegister(args[1]);
        if (x > 0)
            Pointer += y;
        else
            Pointer++;
    };
}
