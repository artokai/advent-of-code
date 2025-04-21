using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2016.D08;

public abstract class Command
{
    public static Command Parse(string instruction)
    {
        return instruction switch
        {
            _ when instruction.StartsWith("rect") => RectCommand.Parse(instruction),
            _ when instruction.StartsWith("rotate row") => RotateRowCommand.Parse(instruction),
            _ when instruction.StartsWith("rotate column") => RotateColumnCommand.Parse(instruction),
            _ => throw new NotImplementedException($"Unknown instruction: {instruction}")
        };
    }

    public abstract void Apply(Map map);
}

public class RectCommand : Command
{
    public int Width { get; }
    public int Height { get; }

    private RectCommand(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static new RectCommand Parse(string instruction)
    {
        var match = Regex.Match(instruction, @"rect (\d+)x(\d+)");
        if (!match.Success)
            throw new ArgumentException("Invalid RectCommand format", nameof(instruction));
        return new RectCommand(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
    }

    public override void Apply(Map map)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                map.Pixels[x, y] = true;
            }
        }
    }
}

public class RotateRowCommand : Command
{
    public int Row { get; }
    public int Amount { get; }

    private RotateRowCommand(int row, int amount)
    {
        Row = row;
        Amount = amount;
    }

    public static new RotateRowCommand Parse(string instruction)
    {
        var match = Regex.Match(instruction, @"rotate row y=(\d+) by (\d+)");
        if (!match.Success)
            throw new ArgumentException("Invalid RotateRowCommand format", nameof(instruction));
        return new RotateRowCommand(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
    }

    public override void Apply(Map map)
    {
        var oldRow = new bool[map.Width];
        for (int x = 0; x < map.Width; x++)
        {
            oldRow[x] = map.Pixels[x, Row];
        }
        for (int x = 0; x < map.Width; x++)
        {
            map.Pixels[x, Row] = oldRow[(x - Amount + map.Width) % map.Width];
        }
    }
}

public class RotateColumnCommand : Command
{
    public int Column { get; }
    public int Amount { get; }

    private RotateColumnCommand(int column, int amount)
    {
        Column = column;
        Amount = amount;
    }

    public static new RotateColumnCommand Parse(string instruction)
    {
        var match = Regex.Match(instruction, @"rotate column x=(\d+) by (\d+)");
        if (!match.Success)
            throw new ArgumentException("Invalid RotateColumnCommand format", nameof(instruction));
        return new RotateColumnCommand(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
    }

    public override void Apply(Map map)
    {
        var oldColumn = new bool[map.Height];
        for (int y = 0; y < map.Height; y++)
        {
            oldColumn[y] = map.Pixels[Column, y];
        }
        for (int y = 0; y < map.Height; y++)
        {
            map.Pixels[Column, y] = oldColumn[(y - Amount + map.Height) % map.Height];
        }
    }
}
