using System.Diagnostics;

namespace Artokai.AOC.Puzzles.Y2016.D25;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AOC 2016 - Day 25: Clock Signal");
        Console.WriteLine("");

        var swA = Stopwatch.StartNew();
        var partA = new PartA();
        var resultA = partA.Solve();
        swA.Stop();
        Console.WriteLine($"Part A: {resultA} ({swA.ElapsedMilliseconds}ms)");
    }
}
