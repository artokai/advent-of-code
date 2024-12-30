using System.Diagnostics;

namespace Artokai.AOC.Puzzles.Y2024.D04;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AOC 2024 - Day 04: Ceres Search");
        Console.WriteLine("");

        var swA = Stopwatch.StartNew();
        var partA = new PartA();
        var resultA = partA.Solve();
        swA.Stop();
        Console.WriteLine($"Part A: {resultA} ({swA.ElapsedMilliseconds}ms)");

        var swB = Stopwatch.StartNew();
        var partB = new PartB();
        var resultB = partB.Solve();
        swB.Stop();
        Console.WriteLine($"Part B: {resultB} ({swB.ElapsedMilliseconds}ms)");
    }
}