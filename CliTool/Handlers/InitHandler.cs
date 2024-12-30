using Artokai.AOC.CliTool.Utils;
using Microsoft.Extensions.Configuration;

namespace Artokai.AOC.CliTool.Handlers;

public class InitHandler
{
    private IConfiguration _configuration;

    public InitHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task InvokeAsync(int year, int day)
    {
        var targetFolder = $@"..\Puzzles\Y{year}\D{day:D2}";
        if (Directory.Exists(targetFolder))
        {
            throw new CliToolException(
                "Folder already exists",
                $"Solution folder '{targetFolder}' already exists. Please remove it if you want to recreate the solution."
            );
        }

        var templateFolder = $@".\Template";
        if (!Directory.Exists(templateFolder))
        {
            throw new CliToolException(
                "Template folder not found",
                $"Template folder '{templateFolder}' not found. Please make sure the template folder exists."
            );
        }

        var aocClient = new AocClient(_configuration);
        var metadata = await aocClient.FetchPuzzleMetadataAsync(year, day);
        if (metadata == null)
        {
            throw new CliToolException(
                "Failed to fetch puzzle metadata",
                $"Failed to fetch puzzle metadata for year {year} day {day}. Please make sure you have a valid session cookie in your configuration."
            );
        }

        using var inputStream = await aocClient.FetchInputAsync(year, day);
        if (inputStream == null)
        {
            throw new CliToolException(
                "Failed to fetch input",
                $"Failed to fetch input for year {year} day {day}. Please make sure you have a valid session cookie in your configuration."
            );
        }

        // Create target directory
        Directory.CreateDirectory(targetFolder);

        // Copy all files from template folder to target folder recursively
        foreach (var file in Directory.GetFiles(templateFolder, "*.*", SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(templateFolder, file);
            var dirPath = Path.GetDirectoryName(relativePath);
            if (!string.IsNullOrEmpty(dirPath))
            {
                Directory.CreateDirectory(Path.Combine(targetFolder, dirPath));
            }
            var targetFileName = Path.GetFileName(file);
            if (targetFileName == "csproj.template")
            {
                targetFileName = $"AOC_{year}_{day:D2}.csproj";
            }
            var targetPath = string.IsNullOrEmpty(dirPath)
                ? Path.Combine(targetFolder, targetFileName)
                : Path.Combine(targetFolder, dirPath, targetFileName);

            File.Copy(file, targetPath, true);
        }

        // Replace contents in all copied files
        var files = Directory.GetFiles(targetFolder, "*.*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            foreach (var key in metadata.Keys)
            {
                var value = metadata[key]
                    .Replace("&apos;", "'")
                    .Replace("&quot;", "\"")
                    .Replace("&gt;", ">")
                    .Replace("&lt;", "<")
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"");
                content = content.Replace($"%%{key}%%", value);
            }
            File.WriteAllText(file, content);
        }

        // Save input.txt
        using (var sr = new StreamReader(inputStream))
        {
            using (var sw = new StreamWriter(Path.Combine(targetFolder, "input.txt"), false))
            {
                await sr.BaseStream.CopyToAsync(sw.BaseStream);
            }
        };

        // Create a backup of input
        File.Copy(Path.Combine(targetFolder, "input.txt"), Path.Combine(targetFolder, "input_original.txt"), true);
    }
}
