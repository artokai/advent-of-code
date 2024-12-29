using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using Artokai.AOC.CliTool.Utils;

namespace Artokai.AOC.CliTool;

public static class CommandLineBuilderExtensions
{
    public static CommandLineBuilder UseSimpleErrorMessage(this CommandLineBuilder builder)
    {
        builder.AddMiddleware(async (context, next) =>
        {
            try
            {
                await next(context);
            }
            catch (CliToolException ex)
            {
                context.ExitCode = ex.ReturnValue;
                if (!Console.IsOutputRedirected) { Console.ForegroundColor = ConsoleColor.Red; }
                context.Console.Error.Write($"Error: {ex.Message}{Environment.NewLine}");
                if (!string.IsNullOrEmpty(ex.Description))
                {
                    context.Console.Error.Write($"{ex.Description}{Environment.NewLine}");
                }
                if (!Console.IsOutputRedirected) { Console.ResetColor(); }

            }
        }, MiddlewareOrder.ExceptionHandler);

        return builder;
    }
}
