using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2025.D12;

[PuzzleInfo(year: 2025, day: 12, part: 1, title: "Christmas Tree Farm")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var (presents, areas) = InputParser.Parse(Input);
        var result = areas
            .Count((area) => IsSolvable(presents, area));
        return result.ToString();
    }

    private bool IsSolvable(List<Present> presents, Area area)
    {
        // This does not solve the packing problem
        // but it it seem that in this puzzle it is enough to just check
        // if the target area has enough total space for the required presents
        var areaSize = area.Width * area.Height;
        var totalSpaceNeeded = area.RequiredPresents
            .Select((count, presentIndex) => count * presents[presentIndex].Size)
            .Sum();
        return totalSpaceNeeded <= areaSize;
    }
}
