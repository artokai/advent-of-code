using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D11;

[PuzzleInfo(year: 2025, day: 11, part: 2, title: "Reactor")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var nodes = InputParser.Parse(Input);

        var svr_dac = PathFinder.DFS(nodes, nodes["svr"], nodes["dac"], nodes["fft"]);
        var dac_fft = PathFinder.DFS(nodes, nodes["dac"], nodes["fft"], nodes["out"]);
        var fft_out = PathFinder.DFS(nodes, nodes["fft"], nodes["out"], nodes["dac"]);
        var svr_dac_fft_out = svr_dac * dac_fft * fft_out;

        var svr_fft = PathFinder.DFS(nodes, nodes["svr"], nodes["fft"], nodes["dac"]);
        var fft_dac = PathFinder.DFS(nodes, nodes["fft"], nodes["dac"], nodes["out"]);
        var dac_out = PathFinder.DFS(nodes, nodes["dac"], nodes["out"], nodes["fft"]);
        var svr_fft_dac_out = svr_fft * fft_dac * dac_out;

        var total = svr_dac_fft_out + svr_fft_dac_out;
        return total.ToString();
    }
}
