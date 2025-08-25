namespace Artokai.AOC.Puzzles.Y2017.D23;

public class Emulator
{
    private Dictionary<string, long> registers = new();
    private List<Action> instructions = new();
    private int pointer = 0;
    public int MulCount { get; private set; } = 0;

    public Emulator(List<string> program)
    {
        pointer = 0;
        instructions = program
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrEmpty(line))
            .Where(line => !line.StartsWith("#"))
            .Select(ParseLine)
            .ToList();
    }

    public void Run()
    {
        while (pointer >= 0 && pointer < instructions.Count)
        {
            instructions[pointer]();
        }
    }

    public void SetRegisterValue(string register, long value)
    {
        registers[register] = value;
    }

    public long GetRegisterOrValue(string valueOrRegister)
    {
        if (long.TryParse(valueOrRegister, out var value))
            return value;
        return registers.GetValueOrDefault(valueOrRegister, 0);
    }

    private Action ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var op = parts[0];
        var x = parts[1];
        var y = parts[2];

        return op switch
        {
            "set" => () =>
            {
                SetRegisterValue(x, GetRegisterOrValue(y));
                pointer++;
            }
            ,
            "sub" => () =>
            {
                SetRegisterValue(x, registers.GetValueOrDefault(x, 0) - GetRegisterOrValue(y));
                pointer++;
            }
            ,
            "mul" => () =>
            {
                MulCount++;
                SetRegisterValue(x, registers.GetValueOrDefault(x, 0) * GetRegisterOrValue(y));
                pointer++;
            }
            ,
            "jnz" => () =>
            {
                var amount = GetRegisterOrValue(x) == 0
                    ? 1
                    : (int)GetRegisterOrValue(y);
                pointer += amount;
            }
            ,
            _ => throw new NotSupportedException($"Unknown operation: {line}")
        };
    }
}
