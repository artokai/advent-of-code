using System.Diagnostics;

namespace Artokai.AOC.Puzzles.Y2016.D10;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AOC 2016 - Day 10: Balance Bots");
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
