using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2016.D04;

[PuzzleInfo(year: 2016, day: 4, part: 1, title: "Security Through Obscurity")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        return Input.AsLines()
            .Select(Room.Parse)
            .Where(room => room.Checksum == room.CalculateChecksum())
            .Sum(room => room.SectorId)
            .ToString();
    }
}
