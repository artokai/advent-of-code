namespace Artokai.AOC.Puzzles.Y2016.D25;

public interface IInstruction
{
    void Execute(Emulator emulator);
}

public abstract class UpdateRegisterValue : IInstruction
{
    public string Register { get; set; }
    private int _amount;

    public UpdateRegisterValue(string register, int amount)
    {
        Register = register;
        _amount = amount;
    }

    public void Execute(Emulator emulator)
    {
        var currentValue = emulator.GetRegisterValue(Register);
        emulator.SetRegisterValue(Register, currentValue + _amount);
        emulator.IncrementInstructionPointer();
    }
}

public class OutInstruction : IInstruction
{
    public string ValueOrRegister { get; set; }
    public Action<int> OutHandler { get; }

    public OutInstruction(string valueOrRegister, Action<int> outHandler)
    {
        ValueOrRegister = valueOrRegister;
        OutHandler = outHandler;
    }

    public void Execute(Emulator emulator)
    {
        var value = emulator.GetValueOrRegister(ValueOrRegister);
        OutHandler(value);
        emulator.IncrementInstructionPointer();
    }

    public override string ToString() => $"out {ValueOrRegister}";
}

public class NoOperationInstruction : IInstruction
{
    public void Execute(Emulator emulator)
    {
        emulator.IncrementInstructionPointer();
    }

    public override string ToString() => "nop";
}

public class CopyInstruction : IInstruction
{
    public string SourceValueOrRegister { get; set; }
    public string DestinationRegister { get; set; }

    public CopyInstruction(string sourceValueOrRegister, string destination)
    {
        SourceValueOrRegister = sourceValueOrRegister;
        DestinationRegister = destination;
    }

    public void Execute(Emulator emulator)
    {
        var value = emulator.GetValueOrRegister(SourceValueOrRegister);
        if (emulator.IsRegister(DestinationRegister))
        {
            emulator.SetRegisterValue(DestinationRegister, value);
        }
        emulator.IncrementInstructionPointer();
    }

    public override string ToString() => $"cpy {SourceValueOrRegister} {DestinationRegister}";
}

public class IncrementInstruction : UpdateRegisterValue
{
    public IncrementInstruction(string register) : base(register, 1) { }

    public override string ToString() => $"inc {Register}";
}

public class DecrementInstruction : UpdateRegisterValue
{
    public DecrementInstruction(string register) : base(register, -1) { }
    public override string ToString() => $"inc {Register}";

}

public class AddInstruction : IInstruction
{
    public string ValueOrRegister { get; private set; }
    public string TargetRegister { get; private set; }

    public AddInstruction(string valueOrRegister, string targetRegister)
    {
        ValueOrRegister = valueOrRegister;
        TargetRegister = targetRegister;
    }

    public void Execute(Emulator emulator)
    {
        var targetValue = emulator.GetRegisterValue(TargetRegister);
        var amount = emulator.GetValueOrRegister(ValueOrRegister);
        emulator.SetRegisterValue(TargetRegister, targetValue + amount);
        emulator.IncrementInstructionPointer();
    }

    public override string ToString() => $"add {ValueOrRegister} {TargetRegister}";
}

public class JumpIfNotZeroInstruction : IInstruction
{
    public string ValueOrRegister { get; set; }
    public string JumpAmountOrRegister { get; set; }

    public JumpIfNotZeroInstruction(string valueOrRegister, string jumpAmountValueOrRegister)
    {
        ValueOrRegister = valueOrRegister;
        JumpAmountOrRegister = jumpAmountValueOrRegister;
    }

    public void Execute(Emulator emulator)
    {
        var value = emulator.GetValueOrRegister(ValueOrRegister);
        var jumpAmount = value != 0 ? emulator.GetValueOrRegister(JumpAmountOrRegister) : 1;
        emulator.IncrementInstructionPointer(jumpAmount);
    }

    public override string ToString() => $"jnz {ValueOrRegister} {JumpAmountOrRegister}";
}

public class MultiplyInstruction : IInstruction
{
    public string FirstValueOrRegister { get; private set; }
    public string SecondValueOrRegister { get; private set; }

    public MultiplyInstruction(string firstValueOrRegister, string secondValueOrRegister)
    {
        FirstValueOrRegister = firstValueOrRegister;
        SecondValueOrRegister = secondValueOrRegister;
    }

    public void Execute(Emulator emulator)
    {
        var a = emulator.GetRegisterValue(FirstValueOrRegister);
        var b = emulator.GetValueOrRegister(SecondValueOrRegister);
        var result = a * b;
        emulator.SetRegisterValue("m", result);
        emulator.IncrementInstructionPointer();
    }

    public override string ToString() => $"mul {FirstValueOrRegister} {SecondValueOrRegister}";
}

public class ToggleInstruction : IInstruction
{
    public string ValueOrRegister { get; set; }

    public ToggleInstruction(string valueOrRegister)
    {
        ValueOrRegister = valueOrRegister;
    }

    public void Execute(Emulator emulator)
    {
        var instructionPointer = emulator.GetValueOrRegister(ValueOrRegister);
        var targetIndex = emulator.InstructionPointer + instructionPointer;
        if (targetIndex >= 0 && targetIndex < emulator.Instructions.Count)
        {
            emulator.Instructions[targetIndex] = Toggle(emulator.Instructions[targetIndex]);
        }
        emulator.IncrementInstructionPointer();
    }

    private IInstruction Toggle(IInstruction old)
    {
        return old switch
        {
            IncrementInstruction inc => new DecrementInstruction(inc.Register),
            DecrementInstruction dec => new IncrementInstruction(dec.Register),
            ToggleInstruction tgl => new IncrementInstruction(tgl.ValueOrRegister),
            JumpIfNotZeroInstruction jnz => new CopyInstruction(jnz.ValueOrRegister, jnz.JumpAmountOrRegister),
            CopyInstruction copy => new JumpIfNotZeroInstruction(copy.SourceValueOrRegister, copy.DestinationRegister),
            _ => throw new InvalidOperationException($"Cannot toggle instruction of type {old.GetType().Name}")
        };
    }

    public override string ToString() => $"tgl {ValueOrRegister}";
}
