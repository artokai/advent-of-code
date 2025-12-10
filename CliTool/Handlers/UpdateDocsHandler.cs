using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Artokai.AOC.CliTool.Handlers;

public class UpdateDocsHandler
{
    private class PuzzleInfo(int year, int day, string title, string partA, string partB)
    {
        public int Year { get; set; } = year;
        public int Day { get; set; } = day;
        public string Title { get; set; } = title;
        public string PartA { get; set; } = partA;
        public string PartB { get; set; } = partB;
    }

    private IConfiguration _configuration;

    public UpdateDocsHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Invoke()
    {
        var puzzles = GetPuzzles();
        foreach (var year in puzzles.Keys)
        {
            WriteYearlyMarkdown(year, puzzles[year]);
        }
        UpdateReadMe(puzzles);
    }

    private Dictionary<int, Dictionary<int, PuzzleInfo>> GetPuzzles()
    {
        var years = new Dictionary<int, Dictionary<int, PuzzleInfo>>();
        var puzzlesPath = Path.GetFullPath(@"..\Puzzles");

        if (!Directory.Exists(puzzlesPath))
        {
            return years;
        }

        // Find all .cs files in puzzle directories
        var partFiles = Directory.GetFiles(puzzlesPath, "*.cs", SearchOption.AllDirectories);

        foreach (var filePath in partFiles)
        {
            var puzzleInfo = ExtractPuzzleInfoFromFile(filePath);
            if (puzzleInfo == null)
                continue;

            var year = puzzleInfo.Value.Year;
            var day = puzzleInfo.Value.Day;
            var part = puzzleInfo.Value.Part;
            var title = puzzleInfo.Value.Title;
            var filename = Path.GetFileName(filePath);

            if (!years.ContainsKey(year))
            {
                years[year] = new Dictionary<int, PuzzleInfo>();
            }

            if (!years[year].ContainsKey(day))
            {
                years[year].Add(day, new PuzzleInfo(year, day, title, "", ""));
            }

            if (part == 1)
            {
                years[year][day].PartA = filename;
            }
            else if (part == 2)
            {
                years[year][day].PartB = filename;
            }
        }

        return years;
    }

    private (int Year, int Day, int Part, string Title)? ExtractPuzzleInfoFromFile(string filePath)
    {
        try
        {
            var content = File.ReadAllText(filePath);
            
            // Regex to match [PuzzleInfo(...)] with named parameters in any order
            var pattern = @"\[PuzzleInfo\((.*?)\)\]";
            var match = Regex.Match(content, pattern, RegexOptions.Singleline);
            
            if (!match.Success)
                return null;

            var parameters = match.Groups[1].Value;

            // Extract year
            var yearMatch = Regex.Match(parameters, @"year\s*:\s*(\d+)");
            if (!yearMatch.Success)
                return null;
            var year = int.Parse(yearMatch.Groups[1].Value);

            // Extract day
            var dayMatch = Regex.Match(parameters, @"day\s*:\s*(\d+)");
            if (!dayMatch.Success)
                return null;
            var day = int.Parse(dayMatch.Groups[1].Value);

            // Extract part
            var partMatch = Regex.Match(parameters, @"part\s*:\s*(\d+)");
            if (!partMatch.Success)
                return null;
            var part = int.Parse(partMatch.Groups[1].Value);

            // Extract title (quoted string)
            var titleMatch = Regex.Match(parameters, @"title\s*:\s*""([^""]*)""");
            if (!titleMatch.Success)
                return null;
            var title = titleMatch.Groups[1].Value;

            return (year, day, part, title);
        }
        catch
        {
            // Silently skip files that can't be parsed
            return null;
        }
    }

    private int GetPuzzleDaysInYear(int year)
    {
        // Starting from 2025 there are only 12 puzzle days per year
        return year < 2025 ? 25 : 12;
    }

    private void WriteYearlyMarkdown(int year, Dictionary<int, PuzzleInfo> puzzles)
    {
        var sb = new StringBuilder();
        sb.Append($"# Advent of Code - {year}\n");
        sb.Append('\n');
        sb.Append("| Day | Title | Part 1 | Part 2 |\n");
        sb.Append("| --: | :---- | :----- | :----- |\n");

        var puzzleDaysInYear = GetPuzzleDaysInYear(year);
        for (var day = 1; day <= puzzleDaysInYear; day++)
        {
            var puzzleInfo = puzzles.ContainsKey(day) ? puzzles[day] : null;
            var title = (puzzleInfo != null) ? puzzles[day].Title : "???";
            var partA = (puzzleInfo != null && !string.IsNullOrEmpty(puzzleInfo.PartA)) ? $"[Part 1](D{day:D2}/{puzzles[day].PartA})" : "";
            var partB = (puzzleInfo != null && !string.IsNullOrEmpty(puzzleInfo.PartB)) ? $"[Part 2](D{day:D2}/{puzzles[day].PartB})" : "";
            sb.Append($"| {day} | [{title}](https://adventofcode.com/{year}/day/{day}) | {partA} | {partB} |\n");
        }

        var path = $@"..\Puzzles\Y{year}\README.md";
        File.WriteAllText(path, sb.ToString());
    }

    private void UpdateReadMe(Dictionary<int, Dictionary<int, PuzzleInfo>> puzzles)
    {
        var lines = File.ReadAllLines(@"..\README.md");
        var sb = new StringBuilder();

        var shouldSkip = false;
        foreach (var line in lines)
        {
            if (line.StartsWith("## Year"))
            {
                shouldSkip = true;
                var year = puzzles.Keys.OrderByDescending(year => year).First();
                var puzzleDaysInYear = GetPuzzleDaysInYear(year);

                sb.Append($"## Year {year}\n");
                sb.Append('\n');
                sb.Append("| Day | Title | Part 1 | Part 2 |\n");
                sb.Append("| --: | :---- | :----- | :----- |\n");

                for (var day = 1; day <= puzzleDaysInYear; day++)
                {
                    var puzzleInfo = puzzles[year].ContainsKey(day) ? puzzles[year][day] : null;
                    var title = (puzzleInfo != null) ? puzzleInfo.Title : "???";
                    var partA = (puzzleInfo != null && !string.IsNullOrEmpty(puzzleInfo.PartA)) ? $"[Part 1](Puzzles/Y{year}/D{day:D2}/{puzzles[year][day].PartA})" : "";
                    var partB = (puzzleInfo != null && !string.IsNullOrEmpty(puzzleInfo.PartB)) ? $"[Part 2](Puzzles/Y{year}/D{day:D2}/{puzzles[year][day].PartB})" : "";
                    sb.Append($"| {day} | [{title}](https://adventofcode.com/{year}/day/{day}) | {partA} | {partB} |\n");
                }
                sb.Append('\n');
            }
            else if (line.StartsWith("## All years"))
            {
                shouldSkip = true;
                sb.Append(line + "\n");
                sb.Append('\n');

                foreach (var year in puzzles.Keys.OrderByDescending(year => year))
                {
                    var completedDaysCount = puzzles[year].Values.Count();
                    var puzzleDaysInYear = GetPuzzleDaysInYear(year);
                    sb.Append($"- [{year}](Puzzles/Y{year}/README.md) ({completedDaysCount} / {puzzleDaysInYear})\n");
                }
                sb.Append("\n");
            }
            else if (line.StartsWith("## "))
            {
                shouldSkip = false;
            }

            if (!shouldSkip)
            {
                sb.Append(line + "\n");
            }
        }

        File.WriteAllText(@"..\README.md", sb.ToString(), Encoding.UTF8);
    }
}
