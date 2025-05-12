using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D10;

[PuzzleInfo(year: 2016, day: 10, part: 1, title: "Balance Bots")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var targetBotId = int.MinValue;
        Simulation.Run(Input, (botId, lowValue, highValue) =>
        {
            targetBotId = (lowValue == 17 && highValue == 61) ? botId : targetBotId;
        });
        return targetBotId.ToString();
    }
}
