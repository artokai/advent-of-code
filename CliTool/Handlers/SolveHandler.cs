using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Artokai.AOC.CliTool.Utils;
using Artokai.AOC.Core;

namespace Artokai.AOC.CliTool.Handlers;

public class SolveHandler
{
    private IConfiguration _configuration;

    public SolveHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task InvokeAsync(int year, int day)
    {
        var targetFolder = $@"..\Puzzles\Y{year}\D{day:D2}";

        var solvers = Assembly.GetExecutingAssembly().GetTypes()
            .Select(t => new { Type = t, Attr = t.GetCustomAttribute<PuzzleInfoAttribute>() })
            .Where(p => p.Attr != null && p.Attr.Year == year && p.Attr.Day == day)
            .OrderBy(p => p.Attr!.Part)
            .ToList();

        if (solvers.Count() == 0 || !Directory.Exists(targetFolder))
        {
            throw new CliToolException(
                "No solutions found",
                $"No solutions found for year {year} day {day}. Please make sure the solution exists."
            );
        }

        if (!File.Exists(Path.Combine(targetFolder, "input.txt")))
        {
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

        var puzzleInfo = solvers.First().Attr!;
        var partAType = solvers.Where(s => s.Attr!.Part == 1).FirstOrDefault()?.Type ?? null;
        var partBType = solvers.Where(s => s.Attr!.Part == 2).FirstOrDefault()?.Type ?? null;

        Console.WriteLine($"AOC {puzzleInfo.Year} - Day {puzzleInfo.Day:D2}: {puzzleInfo.Title}");
        Console.WriteLine("");
        if (partAType != null)
        {
            var partA = (SolverBase)Activator.CreateInstance(partAType)!;
            partA.InputPath = Path.Combine(targetFolder, "input.txt");
            var swA = Stopwatch.StartNew();
            var resultA = partA.Solve();
            swA.Stop();
            Console.WriteLine($"Part A: {resultA} ({swA.ElapsedMilliseconds}ms)");
        }
        else
        {
            Console.WriteLine($"Part A: <Missing> (0ms)");
        }

        if (partBType != null)
        {
            var partB = (SolverBase)Activator.CreateInstance(partBType)!;
            partB.InputPath = Path.Combine(targetFolder, "input.txt");
            var swB = Stopwatch.StartNew();
            var resultB = partB.Solve();
            swB.Stop();
            Console.WriteLine($"Part B: {resultB} ({swB.ElapsedMilliseconds}ms)");
        }
    }
}
