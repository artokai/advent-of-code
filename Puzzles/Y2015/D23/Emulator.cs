namespace Artokai.AOC.Puzzles.Y2015.D23;

public class Emulator
{
    private readonly Dictionary<string, uint> _registers;
    private readonly List<string> _instructions;
    private int _pointer;

    public Emulator(List<string> instructions)
    {
        _pointer = 0;
        _instructions = instructions;
        _registers = new Dictionary<string, uint>();
        _registers["a"] = 0;
        _registers["b"] = 0;
    }

    public uint GetRegisterValue(string register)
    {
        return _registers[register];
    }

    public void SetRegisterValue(string register, uint value)
    {
        _registers[register] = value;
    }

    public void Run()
    {
        _pointer = 0;
        while (_pointer < _instructions.Count)
        {
            var instruction = _instructions[_pointer];
            var parts = instruction
                .Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            switch (parts[0])
            {
                case "hlf":
                    _registers[parts[1]] /= 2;
                    _pointer++;
                    break;
                case "tpl":
                    _registers[parts[1]] *= 3;
                    _pointer++;
                    break;
                case "inc":
                    _registers[parts[1]]++;
                    _pointer++;
                    break;
                case "jmp":
                    _pointer += int.Parse(parts[1]);
                    break;
                case "jie":
                    if (_registers[parts[1]] % 2 == 0)
                        _pointer += int.Parse(parts[2]);
                    else
                        _pointer++;
                    break;
                case "jio":
                    if (_registers[parts[1]] == 1)
                        _pointer += int.Parse(parts[2]);
                    else
                        _pointer++;
                    break;
            }
        }
    }
}
