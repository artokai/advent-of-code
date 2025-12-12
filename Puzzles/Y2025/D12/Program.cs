using System.Diagnostics;

namespace Artokai.AOC.Puzzles.Y2025.D12;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AOC 2025 - Day 12: Christmas Tree Farm");
        Console.WriteLine("");

        var swA = Stopwatch.StartNew();
        var partA = new PartA();
        var resultA = partA.Solve();
        swA.Stop();
        Console.WriteLine($"Part A: {resultA} ({swA.ElapsedMilliseconds}ms)");
    }
}
