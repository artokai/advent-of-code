using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Artokai.AOC.Core;

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
        var solvers = Assembly.GetExecutingAssembly().GetTypes()
            .Select(t => new { Type = t, Attr = t.GetCustomAttribute<PuzzleInfoAttribute>() })
            .Where(p => p.Attr != null);

        var years = new Dictionary<int, Dictionary<int, PuzzleInfo>>();
        foreach (var pair in solvers)
        {
            var attr = pair.Attr!;
            var year = attr.Year;
            var day = attr.Day;

            if (!years.ContainsKey(year))
            {
                years[year] = new Dictionary<int, PuzzleInfo>();
            }

            if (!years[year].ContainsKey(day))
            {
                years[year].Add(day, new PuzzleInfo(year, day, attr.Title, "", ""));
            }
            var filename = pair.Type.Name + ".cs";
            if (attr.Part == 1)
            {
                years[year][day].PartA = filename;
            }
            if (attr.Part == 2)
            {
                years[year][day].PartB = filename;
            }
        }
        return years;
    }

    private void WriteYearlyMarkdown(int year, Dictionary<int, PuzzleInfo> puzzles)
    {
        var sb = new StringBuilder();
        sb.Append($"# Advent of Code - {year}\n");
        sb.Append('\n');
        sb.Append("| Day | Title | Part 1 | Part 2 |\n");
        sb.Append("| --: | :---- | :----- | :----- |\n");
        for (var day = 1; day <= 25; day++)
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
                sb.Append($"## Year {year}\n");
                sb.Append('\n');
                sb.Append("| Day | Title | Part 1 | Part 2 |\n");
                sb.Append("| --: | :---- | :----- | :----- |\n");
                for (var day = 1; day <= 25; day++)
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
                    sb.Append($"- [{year}](Puzzles/Y{year}/README.md)\n");
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
