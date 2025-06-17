
namespace Artokai.AOC.Core;

public class PuzzleInfoAttribute(int year, int day, int part, string title) : Attribute
{
    public string Title { get; } = title;
    public int Year { get; } = year;
    public int Day { get; } = day;
    public int Part { get; } = part;
}
