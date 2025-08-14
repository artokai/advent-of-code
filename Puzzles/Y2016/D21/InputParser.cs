using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D21;

public static class InputParser
{
    public static IEnumerable<BaseCommand> Parse(PuzzleInput input)
    {
        return input.AsLines().Select(ParseLine);
    }

    public static BaseCommand ParseLine(string line)
    {
        if (line.StartsWith("swap position"))
            return SwapPositionCommand.Parse(line);

        if (line.StartsWith("swap letter"))
            return SwapLetterCommand.Parse(line);

        if (line.StartsWith("rotate left"))
            return RotateLeftCommand.Parse(line);

        if (line.StartsWith("rotate right"))
            return RotateRightCommand.Parse(line);

        if (line.StartsWith("rotate based"))
            return RotateBasedCommand.Parse(line);

        if (line.StartsWith("reverse positions"))
            return ReversePositionsCommand.Parse(line);

        if (line.StartsWith("move position"))
            return MovePositionCommand.Parse(line);

        throw new NotSupportedException($"Unknown command: {line}");
    }
}
