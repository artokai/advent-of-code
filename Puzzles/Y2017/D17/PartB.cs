using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D17;

[PuzzleInfo(year: 2017, day: 17, part: 2, title: "Spinlock")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var stepSize = int.Parse(Input.AsSingleLine());
        var bufferSize = 1;
        var currentPosition = 0;
        var result = 0;
        for (var i = 0; i < 50_000_000; i++)
        {
            // Next position can never be zero since we always insert 
            // the new value AFTER some existing item. So zero always
            // stays at index = 0 and we need to only keep track on
            // what value is stored at index 1.
            var nextPosition = (currentPosition + stepSize) % bufferSize + 1;
            if (nextPosition == 1)
            {
                result = i + 1;
            }
            bufferSize++;
            currentPosition = nextPosition;
        }

        return result.ToString();
    }
}
