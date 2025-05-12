using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D10;

public static class Simulation
{
    public static Dictionary<int, int> Run(PuzzleInput input, Action<int, int, int>? onBotFull = null)
    {
        var (bots, instructionStack) = ParseInput(input);
        var outputs = new Dictionary<int, int>();

        var handleOutputInstruction = (ChipInstruction instruction) =>
        {
            outputs[instruction.TargetId] = instruction.Value;
        };

        var handleBotInstruction = (ChipInstruction instruction) =>
        {
            var bot = bots[instruction.TargetId];
            var newInstructions = bot.AddChip(instruction.Value);
            if (newInstructions.Count > 0)
            {
                var lowInstruction = newInstructions[0];
                var highInstruction = newInstructions[1];

                if (onBotFull != null)
                {
                    onBotFull(bot.Id, lowInstruction.Value, highInstruction.Value);
                }

                instructionStack.Push(highInstruction);
                instructionStack.Push(lowInstruction);
            }
        };

        while (instructionStack.Count > 0)
        {
            var instruction = instructionStack.Pop();
            var handler = instruction.IsOutput ? handleOutputInstruction : handleBotInstruction;
            handler(instruction);
        }
        return outputs;
    }

    public static (Dictionary<int, Bot>, Stack<ChipInstruction>) ParseInput(PuzzleInput input)
    {
        var lines = input.AsLines();
        var bots = lines
            .Where(line => line.StartsWith("bot"))
            .Select(Bot.ParseInput)
            .ToDictionary(bot => bot.Id);

        var chipInstructions = lines
            .Where(line => line.StartsWith("value"))
            .Select(line => line.Split(' '))
            .Select(parts => new ChipInstruction(false, int.Parse(parts[5]), int.Parse(parts[1])));

        var stack = new Stack<ChipInstruction>(chipInstructions);
        return (bots, stack);
    }
}

public record ChipInstruction(bool IsOutput, int TargetId, int Value);

public class Bot
{
    public int Id { get; private set; }
    public int LowTarget { get; private set; }
    public int HighTarget { get; private set; }
    public int ChipInHand { get; private set; } = int.MinValue;
    public bool LowIsOutput { get; private set; }
    public bool HighIsOutput { get; private set; }

    public List<ChipInstruction> AddChip(int value)
    {
        if (ChipInHand < 0)
        {
            ChipInHand = value;
            return new List<ChipInstruction>();
        }

        var result = new List<ChipInstruction>
        {
            new ChipInstruction(LowIsOutput, LowTarget, Math.Min(ChipInHand, value)),
            new ChipInstruction(HighIsOutput, HighTarget, Math.Max(ChipInHand, value))
        };
        ChipInHand = int.MinValue;
        return result;
    }

    public static Bot ParseInput(string line)
    {
        var parts = line.Split(' ');
        var id = int.Parse(parts[1]);
        var lowIsOutput = parts[5] == "output";
        var lowTarget = int.Parse(parts[6]);
        var highIsOutput = parts[10] == "output";
        var highTarget = int.Parse(parts[11]);

        return new Bot
        {
            Id = id,
            LowTarget = lowTarget,
            LowIsOutput = lowIsOutput,
            HighTarget = highTarget,
            HighIsOutput = highIsOutput,
        };
    }
}