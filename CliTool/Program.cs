using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Artokai.AOC.CliTool.Handlers;
using Microsoft.Extensions.Configuration;

namespace Artokai.AOC.CliTool;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var configuration = LoadConfiguration();
        var rootCommand = BuildCommands(configuration);
        var cmd = new CommandLineBuilder(rootCommand)
            .UseSimpleErrorMessage()
            .UseDefaults()
            .Build();
        return await cmd.InvokeAsync(args);
    }

    private static Command BuildCommands(IConfiguration configuration)
    {
        var yearOption = new Option<int>(
            description: "The year of the Advent of Code event",
            aliases: ["--year", "-y"],
            getDefaultValue: () => DateTime.Now.Year
        );

        var dayOption = new Option<int>(
            description: "The day of the Advent of Code event",
            aliases: ["--day", "-d"],
            getDefaultValue: () => DateTime.Now.Day
        );

        // Init
        var initHandler = new InitHandler(configuration);
        var initCommand = new Command(
            name: "init",
            description: "Initialize a new Advent of Code solution"
        );
        initCommand.AddOption(yearOption);
        initCommand.AddOption(dayOption);
        initCommand.SetHandler(initHandler.InvokeAsync, yearOption, dayOption);

        // Fetch
        var fetchHandler = new FetchHandler(configuration);
        var fetchCommand = new Command(
            name: "fetch",
            description: "(Re)fetch input data from adventofcode.com"
        );
        fetchCommand.AddOption(yearOption);
        fetchCommand.AddOption(dayOption);
        fetchCommand.SetHandler(fetchHandler.InvokeAsync, yearOption, dayOption);

        // Solve
        var solveHandler = new SolveHandler(configuration);
        var solveCommand = new Command(
            name: "solve",
            description: "Solve the Advent of Code puzzle"
        );
        solveCommand.AddOption(yearOption);
        solveCommand.AddOption(dayOption);
        solveCommand.SetHandler(solveHandler.InvokeAsync, yearOption, dayOption);


        // Doc
        var updateDocsHandler = new UpdateDocsHandler(configuration);
        var updateDocsCommand = new Command(
            name: "updatedocs",
            description: "Update README files"
        );
        updateDocsCommand.SetHandler(updateDocsHandler.Invoke);

        // Root
        var rootCommand = new RootCommand("Advent of Code CLI Tool");
        rootCommand.AddCommand(initCommand);
        rootCommand.AddCommand(fetchCommand);
        rootCommand.AddCommand(solveCommand);
        rootCommand.AddCommand(updateDocsCommand);

        return rootCommand;
    }

    public static IConfiguration LoadConfiguration() {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true);
        if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase)) {
            builder.AddUserSecrets<Program>(optional: true);
        }
        builder.AddEnvironmentVariables();
        return builder.Build();
    }
}
