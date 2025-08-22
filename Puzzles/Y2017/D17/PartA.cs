using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D17;

[PuzzleInfo(year: 2017, day: 17, part: 1, title: "Spinlock")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var stepSize = int.Parse(Input.AsSingleLine());
        var buffer = new List<int> { 0 };
        var currentPosition = 0;
        for (var i = 0; i < 2017; i++)
        {
            var nextPosition = (currentPosition + stepSize) % buffer.Count + 1;
            buffer.Insert(nextPosition, i + 1);
            currentPosition = nextPosition;
        }
        var resultPosition = (currentPosition + 1) % buffer.Count;
        return buffer[resultPosition].ToString();
    }
}
