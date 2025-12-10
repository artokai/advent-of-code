using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Artokai.AOC.CliTool.Utils;

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
        var targetFolder = Path.GetFullPath($@"..\Puzzles\Y{year}\D{day:D2}");

        if (!Directory.Exists(targetFolder))
        {
            throw new CliToolException(
                "No solutions found",
                $"No solutions found for year {year} day {day}. Please make sure the solution exists."
            );
        }

        // Fetch input if needed
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
            }
            File.Copy(Path.Combine(targetFolder, "input.txt"), Path.Combine(targetFolder, "input_original.txt"), true);
        }

        // Find or build puzzle executable
        var exePath = FindPuzzleExecutable(targetFolder, year, day);
        if (exePath == null)
        {
            exePath = await BuildPuzzleProject(targetFolder, year, day);
        }

        // Run the puzzle executable
        await RunPuzzleExecutable(exePath);
    }

    private string? FindPuzzleExecutable(string targetFolder, int year, int day)
    {
        var binDebugPath = Path.Combine(targetFolder, "bin", "Debug");
        
        if (!Directory.Exists(binDebugPath))
        {
            return null;
        }

        var frameworkFolders = Directory.GetDirectories(binDebugPath);
        var exePath = frameworkFolders
            .Select(f => Path.Combine(f, $"AOC_{year}_{day:D2}.exe"))
            .FirstOrDefault(File.Exists);

        return exePath;
    }

    private async Task<string> BuildPuzzleProject(string targetFolder, int year, int day)
    {
        var projectFile = Path.Combine(targetFolder, $"AOC_{year}_{day:D2}.csproj");

        if (!File.Exists(projectFile))
        {
            throw new CliToolException(
                "Project file not found",
                $"Could not find project file at {projectFile}"
            );
        }

        Console.WriteLine($"Building puzzle {year}-{day:D2}...");

        var processInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"build \"{projectFile}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = targetFolder
        };

        using (var process = Process.Start(processInfo))
        {
            if (process == null)
            {
                throw new CliToolException(
                    "Build failed",
                    $"Failed to start build process for {year}-{day:D2}"
                );
            }

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new CliToolException(
                    "Build failed",
                    $"Failed to build puzzle {year}-{day:D2}:\nStdout: {output}\nStderr: {error}"
                );
            }
        }

        // After successful build, find the executable
        var exePath = FindPuzzleExecutable(targetFolder, year, day);
        if (exePath == null)
        {
            throw new CliToolException(
                "Build succeeded but executable not found",
                $"Could not find executable after building {year}-{day:D2}"
            );
        }

        return exePath;
    }

    private async Task RunPuzzleExecutable(string exePath)
    {
        var puzzleDir = Path.GetDirectoryName(exePath);
        
        var processInfo = new ProcessStartInfo
        {
            FileName = exePath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = puzzleDir
        };

        using (var process = Process.Start(processInfo))
        {
            if (process == null)
            {
                throw new CliToolException(
                    "Execution failed",
                    $"Failed to start puzzle executable at {exePath}"
                );
            }

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            // Display output from the puzzle
            Console.WriteLine(output);

            if (process.ExitCode != 0)
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Console.Error.WriteLine(error);
                }
                throw new CliToolException(
                    "Puzzle execution failed",
                    $"Puzzle exited with code {process.ExitCode}"
                );
            }
        }
    }
}
