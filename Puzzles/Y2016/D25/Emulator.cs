using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D25;

public class Emulator
{
    public bool Running { get; private set; }

    public int InstructionPointer { get; set; } = 0;

    private Action<Emulator, int>? OutHandler { get; init; }

    public List<IInstruction> Instructions { get; set; } = new();

    private readonly Dictionary<string, int> _registers = new()
    {
        { "a", 0 },
        { "b", 0 },
        { "c", 0 },
        { "d", 0 },

        // Register for multiplication result
        { "m", 0 }
    };

    public Emulator(PuzzleInput input, Action<Emulator, int>? outHandler = null) : this(input.AsLines(), outHandler)
    {
    }

    public Emulator(IEnumerable<string> input, Action<Emulator, int>? outHandler = null)
    {
        OutHandler = outHandler ?? ((_, value) => Console.WriteLine(value));
        Instructions = input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Where(line => !line.StartsWith("#"))
            .Select(ParseInstruction)
            .Where(instruction => instruction != null)
            .Cast<IInstruction>()
            .ToList();

        Reset();
    }

    public void Run()
    {
        Running = true;
        InstructionPointer = 0;
        while (Running && InstructionPointer < Instructions.Count)
        {
            var instruction = Instructions[InstructionPointer];
            instruction.Execute(this);
        }
    }

    public void Stop()
    {
        Running = false;
    }

    public void HandleOutInstruction(int value)
    {
        if (OutHandler == null)
        {
            Console.WriteLine(value);
            return;
        }

        OutHandler(this, value);
    }

    private IInstruction? ParseInstruction(string line)
    {
        var parts = line.Split(' ');
        return parts[0] switch
        {
            "cpy" => new CopyInstruction(parts[1], parts[2]),
            "inc" => new IncrementInstruction(parts[1]),
            "dec" => new DecrementInstruction(parts[1]),
            "jnz" => new JumpIfNotZeroInstruction(parts[1], parts[2]),
            "tgl" => new ToggleInstruction(parts[1]),

            // out instruction for 2016/25
            "out" => new OutInstruction(parts[1], HandleOutInstruction),

            // Additional instructions for part 2016/23 part 2
            "nop" => new NoOperationInstruction(),
            "add" => new AddInstruction(parts[1], parts[2]),
            "mul" => new MultiplyInstruction(parts[1], parts[2]),
            _ => throw new InvalidOperationException($"Unknown instruction: {line}")
        };
    }

    public bool IsRegister(string valueOrRegister) => valueOrRegister.Length == 1 && char.IsLower(valueOrRegister[0]);

    public int GetRegisterValue(string register) => _registers.GetValueOrDefault(register, 0);

    public void SetRegisterValue(string register, int value)
    {
        _registers[register] = value;
    }

    public void IncrementInstructionPointer(int increment = 1)
    {
        InstructionPointer += increment;
    }

    public int GetValueOrRegister(string valueOrRegister)
    {
        var isRegister = valueOrRegister.Length == 1 && char.IsLower(valueOrRegister[0]);
        return isRegister
            ? GetRegisterValue(valueOrRegister)
            : int.Parse(valueOrRegister);
    }

    public void Reset()
    {
        InstructionPointer = 0;
        foreach (var key in _registers.Keys.ToList())
        {
            _registers[key] = 0;
        }
    }

    public void PrintInstructions()
    {
        foreach (var instruction in Instructions)
        {
            Console.WriteLine(instruction);
        }
    }
}
