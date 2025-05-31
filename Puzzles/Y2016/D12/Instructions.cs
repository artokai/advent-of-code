namespace Artokai.AOC.Puzzles.Y2016.D12;

public interface IInstruction
{
    void Execute(Emulator emulator);
}

public abstract class UpdateRegisterValue : IInstruction
{
    private string _register;
    private int _amount;

    public UpdateRegisterValue(string register, int amount)
    {
        _register = register;
        _amount = amount;
    }

    public void Execute(Emulator emulator)
    {
        var currentValue = emulator.GetRegisterValue(_register);
        emulator.SetRegisterValue(_register, currentValue + _amount);
        emulator.IncrementInstructionPointer();
    }
}

public class CopyInstruction : IInstruction
{
    private readonly string _srcValueOrRegister;
    private readonly string _dstRegister;

    public CopyInstruction(string sourceValueOrRegister, string destination)
    {
        _srcValueOrRegister = sourceValueOrRegister;
        _dstRegister = destination;
    }

    public void Execute(Emulator emulator)
    {
        var value = emulator.GetValueOrRegister(_srcValueOrRegister);
        emulator.SetRegisterValue(_dstRegister, value);
        emulator.IncrementInstructionPointer();
    }
}

public class IncrementInstruction : UpdateRegisterValue
{
    public IncrementInstruction(string register) : base(register, 1) { }
}

public class DecrementInstruction : UpdateRegisterValue
{
    public DecrementInstruction(string register) : base(register, -1) { }
}

public class JumpIfNotZeroInstruction : IInstruction
{
    private string _valueOrRegister;
    private int _jumpAmount;

    public JumpIfNotZeroInstruction(string valueOrRegister, int jumpAmount)
    {
        _valueOrRegister = valueOrRegister;
        _jumpAmount = jumpAmount;
    }

    public void Execute(Emulator emulator)
    {
        var value = emulator.GetValueOrRegister(_valueOrRegister);
        var jumpAmount = value != 0 ? _jumpAmount : 1;
        emulator.IncrementInstructionPointer(jumpAmount);
    }
}
