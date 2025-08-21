using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D16;

[PuzzleInfo(year: 2017, day: 16, part: 2, title: "Permutation Promenade")]
public class PartB : SolverBase
{
    private Dictionary<string, DanceResult> memo = new();

    public override string Solve()
    {
        var choreography = InputParser.Parse(Input);
        var dancers = "abcdefghijklmnop".ToCharArray();
        var totalIterations = 1_000_000_000;
        for (var i = 0; i < totalIterations; i++)
        {
            var danceResult = PerformDance(i, dancers, choreography);
            dancers = danceResult.result;
            if (danceResult.firstSeenIteration < i)
            {
                // If we hit a repetition, we can skip forward n * cycleLength times
                var cycleLength = i - danceResult.firstSeenIteration;
                var remainingIterations = totalIterations - i - 1;
                var dancesStillNeeded = remainingIterations % cycleLength;
                for (var j = 0; j < dancesStillNeeded; j++)
                {
                    danceResult = PerformDance(i + j + 1, dancers, choreography);
                    dancers = danceResult.result;
                }
                break;
            }
        }
        return new string(dancers);
    }

    public DanceResult PerformDance(int iteration, char[] dancers, List<Func<char[], char[]>> choreography)
    {
        var key = new string(dancers);
        if (!memo.ContainsKey(key))
        {
            foreach (var move in choreography)
            {
                dancers = move(dancers);
            }
            memo[key] = new DanceResult(dancers, iteration);
        }
        return memo[key];
    }
}

public record struct DanceResult(char[] result, int firstSeenIteration);
