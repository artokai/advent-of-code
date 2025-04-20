using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D22;

[PuzzleInfo(year: 2015, day: 22, part: 1, title: "Wizard Simulator 20XX")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var game = new Game(Input, isPartB: false);
        return game.Play().ToString();
    }
}
