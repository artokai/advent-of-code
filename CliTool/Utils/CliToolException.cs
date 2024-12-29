namespace Artokai.AOC.CliTool.Utils;

public class CliToolException : Exception
{
    public string Description { get; init; }
    public int ReturnValue { get; init; }

    public CliToolException(string message, string description = "", int returnValue = 1) : base(message) { 
        Description = description;
        ReturnValue = returnValue;
    }
}
