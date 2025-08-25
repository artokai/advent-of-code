using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D21;

[PuzzleInfo(year: 2017, day: 21, part: 2, title: "Fractal Art")]
public class PartB : SolverBase
{
    private Dictionary<string, int> memo = new();
    private Enhancer enhancer = new Enhancer(new Dictionary<int, Dictionary<string, Grid>>());

    public override string Solve()
    {
        var current = Grid.Parse(".#./..#/###");
        var patterns = InputParser.Parse(Input);
        enhancer = new Enhancer(patterns);

        var result = EnhanceAndCount(current, 18);
        return result.ToString();
    }

    private int EnhanceAndCount(Grid current, int iterations)
    {
        if (iterations == 0)
        {
            return current.CountActiveCells();
        }

        if (current.Size == 6)
        {
            return EnhanceAndCount(current.ExtractSubGrid(0, 0, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(2, 0, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(4, 0, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(0, 2, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(2, 2, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(4, 2, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(0, 4, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(2, 4, 2), iterations)
                 + EnhanceAndCount(current.ExtractSubGrid(4, 4, 2), iterations);
        }

        var memoKey = current.ToString() + "|" + iterations;
        if (memo.ContainsKey(memoKey))
        {
            return memo[memoKey];
        }

        var next = enhancer.Enhance(current);
        var result = EnhanceAndCount(next, iterations - 1);
        memo[memoKey] = result;
        return result;
    }
}
