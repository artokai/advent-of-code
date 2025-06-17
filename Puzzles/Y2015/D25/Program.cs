using System.Diagnostics;

namespace Artokai.AOC.Puzzles.Y2015.D25;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AOC 2015 - Day 25: Let It Snow");
        Console.WriteLine("");

        var swA = Stopwatch.StartNew();
        var partA = new PartA();
        var resultA = partA.Solve();
        swA.Stop();
        Console.WriteLine($"Part A: {resultA} ({swA.ElapsedMilliseconds}ms)");
    }
}
