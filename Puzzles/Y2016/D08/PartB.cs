using System.Text;
using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D08;

[PuzzleInfo(year: 2016, day: 8, part: 2, title: "Two-Factor Authentication")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var map = new Map(50, 6);
        var instructions = Input.AsLines();
        foreach(var instruction in instructions) {
            var cmd = Command.Parse(instruction);
            cmd.Apply(map);
        }

        Console.WriteLine(new string('-', 50));
        Console.Write(map.ToString());
        Console.WriteLine(new string('-', 50));
        return "See output";
    }
}
