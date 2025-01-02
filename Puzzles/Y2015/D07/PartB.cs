using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D07;

[PuzzleInfo(year: 2015, day: 7, part: 2, title: "Some Assembly Required")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var pass1 = new Circuit(Input);
        pass1.Simulate();
        var aValue = pass1.Lines["a"];

        var pass2 = new Circuit(Input);
        var bSetter = pass2.Gates.Find(g => g.Output.LineKey == "b");
        if (bSetter is RedirectGate rg)
        {
            rg.Input = new InOut(aValue.ToString(), pass2.Lines);
        }
        pass2.Simulate();
        var result = pass2.Lines["a"];
        return result.ToString();
    }
}
