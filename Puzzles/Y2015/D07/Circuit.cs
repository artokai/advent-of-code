using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2015.D07;

public class Circuit
{
    public Dictionary<string, ushort> Lines { get; init; }

    public List<Gate> Gates { get; init; }

    public Circuit(PuzzleInput input)
    {
        Lines = new Dictionary<string, ushort>();
        Gates = new List<Gate>();

        var re = new Regex(@"(?:(?<inputA>[a-z0-9]+)\s+)?(?<Operator>[A-Za-z0-9]+)(?:\s+(?<inputB>[a-z0-9]+))? -> (?<output>\S+)");
        input.AsLines().ForEach(line =>
        {
            var match = re.Match(line);
            if (!match.Success) throw new Exception($"Invalid line: {line}");

            var inputA = match.Groups["inputA"].Value;
            var inputB = match.Groups["inputB"].Value;
            var op = match.Groups["Operator"].Value;
            var output = match.Groups["output"].Value;

            switch (op)
            {
                case "AND":
                    Gates.Add(new AndGate(Lines, inputA, inputB, output));
                    break;
                case "OR":
                    Gates.Add(new OrGate(Lines, inputA, inputB, output));
                    break;
                case "NOT":
                    Gates.Add(new NotGate(Lines, inputB, output));
                    break;
                case "LSHIFT":
                    Gates.Add(new LShiftGate(Lines, inputA, inputB, output));
                    break;
                case "RSHIFT":
                    Gates.Add(new RShiftGate(Lines, inputA, inputB, output));
                    break;
                default:
                    Gates.Add(new RedirectGate(Lines, op, output));
                    break;
            }
        });
    }

    public void Simulate()
    {
        var iters = 0;
        var changed = true;
        while (changed && iters <= 1000)
        {
            iters++;
            if (iters >= 1000) throw new Exception("Too many iterations");
            changed = false;
            foreach (var gate in Gates)
            {
                changed |= gate.Evaluate();
            }
        }
    }
}

public class InOut
{
    private ushort _constantValue = 0;

    private Dictionary<string, ushort>? _allLines;

    public string? LineKey { get; set; }

    public InOut(string lineOrConstant, Dictionary<string, ushort> allLines)
    {
        if (ushort.TryParse(lineOrConstant, out var value))
        {
            _constantValue = value;
        }
        else
        {
            LineKey = lineOrConstant;
            _allLines = allLines;
        }
    }

    public ushort Value
    {
        get => LineKey != null ? _allLines!.GetValueOrDefault(LineKey, (ushort)0) : _constantValue;
        set { if (LineKey != null) { _allLines![LineKey] = value; } else { _constantValue = value; } }
    }

}

public abstract class Gate
{
    public InOut Output { get; init; }

    public Gate(Dictionary<string, ushort> lines, string output)
    {
        Output = new InOut(output, lines);
    }

    public abstract bool Evaluate();
}

public class AndGate : Gate
{
    public InOut InputA { get; }
    public InOut InputB { get; }

    public AndGate(Dictionary<string, ushort> lines, string inputA, string inputB, string output) : base(lines, output)
    {
        InputA = new InOut(inputA, lines);
        InputB = new InOut(inputB, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = (ushort)(InputA.Value & InputB.Value);
        return Output.Value != prevValue;
    }
}

public class OrGate : Gate
{
    public InOut InputA { get; }
    public InOut InputB { get; }

    public OrGate(Dictionary<string, ushort> lines, string inputA, string inputB, string output) : base(lines, output)
    {
        InputA = new InOut(inputA, lines);
        InputB = new InOut(inputB, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = (ushort)(InputA.Value | InputB.Value);
        return Output.Value != prevValue;
    }
}

public class NotGate : Gate
{
    public InOut Input { get; }

    public NotGate(Dictionary<string, ushort> lines, string input, string output) : base(lines, output)
    {
        Input = new InOut(input, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = (ushort)~Input.Value;
        return Output.Value != prevValue;
    }
}

public class RedirectGate : Gate
{
    public InOut Input { get; set; }

    public RedirectGate(Dictionary<string, ushort> lines, string input, string output) : base(lines, output)
    {
        Input = new InOut(input, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = Input.Value;
        return Output.Value != prevValue;
    }
}

public class LShiftGate : Gate
{
    public InOut InputA { get; }
    public InOut InputB { get; }

    public LShiftGate(Dictionary<string, ushort> lines, string inputA, string inputB, string output) : base(lines, output)
    {
        InputA = new InOut(inputA, lines);
        InputB = new InOut(inputB, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = (ushort)(InputA.Value << InputB.Value);
        return Output.Value != prevValue;
    }
}

public class RShiftGate : Gate
{
    public InOut InputA { get; }
    public InOut InputB { get; }

    public RShiftGate(Dictionary<string, ushort> lines, string inputA, string inputB, string output) : base(lines, output)
    {
        InputA = new InOut(inputA, lines);
        InputB = new InOut(inputB, lines);
    }

    public override bool Evaluate()
    {
        var prevValue = Output.Value;
        Output.Value = (ushort)(InputA.Value >> InputB.Value);
        return Output.Value != prevValue;
    }
}
