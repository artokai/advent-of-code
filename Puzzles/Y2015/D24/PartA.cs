using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2015.D24;

[PuzzleInfo(year: 2015, day: 24, part: 1, title: "It Hangs in the Balance")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var groupCount = 3;
        var packages = Input.AsListOf<int>();
        var totalWeight = packages.Sum();
        var targetWeight = totalWeight / groupCount;

        var splits = Shared.GetGroupsOfTargetWeight(targetWeight, packages);
        var minimalGroups = Shared.FindMinimalGroups(splits, groupCount);
        var result = Shared.GetMinimalQuantumEntanglement(minimalGroups, packages);
        return result.ToString();
    }
}
