namespace Artokai.AOC.Puzzles.Y2016.D21;

public abstract class BaseCommand
{
    public abstract void Execute(char[] code);
    public abstract void Revert(char[] code);

    protected void Swap(char[] code, int pos1, int pos2)
    {
        var tmp = code[pos1];
        code[pos1] = code[pos2];
        code[pos2] = tmp;
    }

    protected void RotateRight(char[] code, int steps)
    {
        steps %= code.Length;
        for (int i = 0; i < steps; i++)
        {
            var last = code[^1];
            for (int j = code.Length - 1; j > 0; j--)
                code[j] = code[j - 1];
            code[0] = last;
        }
    }

    protected void RotateLeft(char[] code, int steps)
    {
        steps %= code.Length;
        for (int i = 0; i < steps; i++)
        {
            var first = code[0];
            for (int j = 0; j < code.Length - 1; j++)
                code[j] = code[j + 1];
            code[^1] = first;
        }
    }
}


public class SwapPositionCommand(int Position1, int Position2) : BaseCommand
{
    public static SwapPositionCommand Parse(string instruction)
    {
        var parts = instruction.Split(' ');
        return new(int.Parse(parts[2]), int.Parse(parts[5]));
    }

    public override void Execute(char[] code) => Swap(code, Position1, Position2);
    public override void Revert(char[] code) => Execute(code);
}

public class SwapLetterCommand(char Letter1, char Letter2) : BaseCommand
{
    public static SwapLetterCommand Parse(string instruction)
    {
        var parts = instruction.Split(' ');
        return new(parts[2][0], parts[5][0]);
    }

    public override void Execute(char[] code) => Swap(code, Array.IndexOf(code, Letter1), Array.IndexOf(code, Letter2));
    public override void Revert(char[] code) => Execute(code);
}

public class RotateLeftCommand(int Steps) : BaseCommand
{
    public static RotateLeftCommand Parse(string instruction) => new(int.Parse(instruction.Split(' ')[2]));

    public override void Execute(char[] code) => RotateLeft(code, Steps);
    public override void Revert(char[] code) => RotateRight(code, Steps);
}

public class RotateRightCommand(int Steps) : BaseCommand
{
    public static RotateRightCommand Parse(string instruction) => new(int.Parse(instruction.Split(' ')[2]));

    public override void Execute(char[] code) => RotateRight(code, Steps);
    public override void Revert(char[] code) => RotateLeft(code, Steps);
}

public class RotateBasedCommand(char Letter) : BaseCommand
{
    public static RotateBasedCommand Parse(string instruction) => new(instruction.Split(' ')[6][0]);

    public override void Execute(char[] code)
    {
        var index = Array.IndexOf(code, Letter);
        var steps = GetSteps(index);
        RotateRight(code, steps);
    }

    public override void Revert(char[] code)
    {
        var currentIndex = Array.IndexOf(code, Letter);
        var steps = FindStepsForRevert(code, currentIndex);
        RotateLeft(code, steps);
    }

    private int GetSteps(int index) => 1 + index + (index >= 4 ? 1 : 0);
    private int FindStepsForRevert(char[] code, int currentPos)
    {
        for (var potentialOriginalIndex = 0; potentialOriginalIndex < code.Length; potentialOriginalIndex++)
        {
            var steps = GetSteps(potentialOriginalIndex);
            var newPosition = (potentialOriginalIndex + steps) % code.Length;
            if (newPosition == currentPos)
                return steps;
        }
        throw new InvalidOperationException("No matching position found.");
    }
}

public class ReversePositionsCommand(int Start, int End) : BaseCommand
{
    public static ReversePositionsCommand Parse(string instruction)
    {
        var parts = instruction.Split(' ');
        return new(int.Parse(parts[2]), int.Parse(parts[4]));
    }

    public override void Execute(char[] code) => Array.Reverse(code, Start, End - Start + 1);
    public override void Revert(char[] code) => Execute(code);
}

public class MovePositionCommand(int From, int To) : BaseCommand
{
    public static MovePositionCommand Parse(string instruction)
    {
        var parts = instruction.Split(' ');
        return new(int.Parse(parts[2]), int.Parse(parts[5]));
    }

    public override void Execute(char[] code) => Move(code, From, To);
    public override void Revert(char[] code) => Move(code, To, From);

    private void Move(char[] code, int from, int to)
    {
        var letter = code[from];
        for (int i = from; i < code.Length - 1; i++)
            code[i] = code[i + 1];
        for (int i = code.Length - 1; i > to; i--)
            code[i] = code[i - 1];
        code[to] = letter;
    }
}
