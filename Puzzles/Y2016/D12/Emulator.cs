using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D12;

public class Emulator
{
    private int _instructionPointer = 0;

    private List<IInstruction> _instructions = new();

    private readonly Dictionary<string, int> _registers = new()
    {
        { "a", 0 },
        { "b", 0 },
        { "c", 0 },
        { "d", 0 }
    };

    public Emulator(PuzzleInput input)
    {
        _instructions = input.AsLines()
            .Select(ParseInstruction)
            .Where(instruction => instruction != null)
            .Cast<IInstruction>()
            .ToList();

        Reset();
    }

    public void Run()
    {
        while (_instructionPointer < _instructions.Count)
        {
            var instruction = _instructions[_instructionPointer];
            instruction.Execute(this);
        }
    }

    private IInstruction? ParseInstruction(string line)
    {
        var parts = line.Split(' ');
        return parts[0] switch
        {
            "cpy" => new CopyInstruction(parts[1], parts[2]),
            "inc" => new IncrementInstruction(parts[1]),
            "dec" => new DecrementInstruction(parts[1]),
            "jnz" => new JumpIfNotZeroInstruction(parts[1], int.Parse(parts[2])),
            _ => null
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
        _instructionPointer += increment;
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
        _instructionPointer = 0;
        foreach (var key in _registers.Keys.ToList())
        {
            _registers[key] = 0;
        }
    }

}
