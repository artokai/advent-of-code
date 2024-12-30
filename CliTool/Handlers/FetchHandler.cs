using Artokai.AOC.CliTool.Utils;
using Microsoft.Extensions.Configuration;

namespace Artokai.AOC.CliTool.Handlers;

public class FetchHandler
{
    private IConfiguration _configuration;

    public FetchHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task InvokeAsync(int year, int day)
    {
        var targetFolder = $@"..\Puzzles\Y{year}\D{day:D2}";
        if (!Directory.Exists(targetFolder))
        {
            throw new CliToolException(
                "No solutions found",
                $"No solutions found for year {year} day {day}. Please make sure the solution exists."
            );
        }

        var aocClient = new AocClient(_configuration);
        using var inputStream = await aocClient.FetchInputAsync(year, day);
        if (inputStream == null)
        {
            throw new CliToolException(
                "Failed to fetch input",
                $"Failed to fetch input for year {year} day {day}. Please make sure you have a valid session cookie in your configuration."
            );
        }
        using (var sr = new StreamReader(inputStream))
        {
            using (var sw = new StreamWriter(Path.Combine(targetFolder, "input.txt"), false))
            {
                await sr.BaseStream.CopyToAsync(sw.BaseStream);
            }
        };
        File.Copy(Path.Combine(targetFolder, "input.txt"), Path.Combine(targetFolder, "input_original.txt"), true);
    }
}
