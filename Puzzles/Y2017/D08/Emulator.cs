namespace Artokai.AOC.Puzzles.Y2017.D08;

public class Emulator
{
    public Dictionary<string, int> Registers { get; private set; } = new Dictionary<string, int>();
    public int Pointer { get; private set; }
    public List<Instruction> Instructions { get; private set; }
    public bool Running { get; private set; }
    public (string Register, int RegisterValue)? MaxSeenValue;

    public Emulator(IEnumerable<string> program)
    {
        Pointer = 0;
        Running = false;
        Registers = new Dictionary<string, int>();
        MaxSeenValue = null;
        Instructions = program
            .Select(l => ParseInstruction(l))
            .ToList();
    }

    public void Run()
    {
        Running = true;
        while (Running && Pointer < Instructions.Count)
        {
            var instruction = Instructions[Pointer];
            instruction.Execute(this);
            Pointer++;
        }
        Running = false;
    }

    public Instruction ParseInstruction(string line)
    {
        var parts = line.Split(' ');
        var operandA = parts[0];
        var operation = parts[1];
        var operandB = parts[2];
        var condition = parts.Length > 3
            ? new InstructionCondition(parts[5], parts[4], parts[6])
            : null;
        return new Instruction(operation, operandA, operandB, condition);
    }

    public int GetOperandValue(string operand)
    {
        if (int.TryParse(operand, out var value))
            return value;
        return GetRegisterValue(operand);
    }

    public int GetRegisterValue(string register)
    {
        return Registers.GetValueOrDefault(register, 0);
    }
    public void SetRegisterValue(string register, int value)
    {
        var maxSeen = MaxSeenValue.HasValue
            ? MaxSeenValue.Value.RegisterValue
            : int.MinValue;

        if (value > maxSeen)
        {
            MaxSeenValue = (register, value);
        }

        Registers[register] = value;
    }
}


public record class Instruction(string Operation, string OperandA, string OperandB, InstructionCondition? Condition)
{
    public void Execute(Emulator emulator)
    {
        var targetRegister = OperandA;
        if (Condition == null || Condition.Evaluate(emulator))
        {
            var oldValue = emulator.GetRegisterValue(targetRegister);
            var amount = emulator.GetOperandValue(OperandB);
            switch (Operation)
            {
                case "inc":
                    emulator.SetRegisterValue(targetRegister, oldValue + amount);
                    break;
                case "dec":
                    emulator.SetRegisterValue(targetRegister, oldValue - amount);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid operation: {Operation}");
            }
        }
    }

}

public record class InstructionCondition(string Operator, string OperandA, string OperandB)
{
    public bool Evaluate(Emulator emulator)
    {
        var a = emulator.GetOperandValue(OperandA);
        var b = emulator.GetOperandValue(OperandB);
        return Operator switch
        {
            "<" => a < b,
            "<=" => a <= b,
            ">" => a > b,
            ">=" => a >= b,
            "==" => a == b,
            "!=" => a != b,
            _ => throw new InvalidOperationException($"Invalid operator: {Operator}")
        };
    }
}
