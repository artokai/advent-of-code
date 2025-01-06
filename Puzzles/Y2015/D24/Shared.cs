namespace Artokai.AOC.Puzzles.Y2015.D24;

public static class Shared
{
    public static List<List<int>> GetGroupsOfTargetWeight(int targetWeight, List<int> packages)
    {
        // Get all possible groups of packages that sum up to targetWeight
        // At this point we don't care if the remaining packages can also be
        // split into balanced gorups or not.
        //
        // Note: We store package indexes instead of the actual package weights
        // to make it possible distinguish between packages of the same weight
        var result = new List<List<int>>();
        var stack = new Stack<(List<int> Selected, int TargetWeight, int StartIndex)>();
        stack.Push((new List<int>(), targetWeight, 0));
        while (stack.Count > 0)
        {
            (var selected, var currentTarget, var startIndex) = stack.Pop();

            if (currentTarget == 0)
            {
                result.Add(selected);
                continue;
            }

            for (int i = startIndex; i < packages.Count; i++)
            {
                if (packages[i] <= currentTarget)
                {
                    var newSelected = new List<int>(selected) { i };
                    stack.Push((newSelected, currentTarget - packages[i], i + 1));
                }
            }
        }
        return result;
    }

    public static List<List<int>> FindMinimalGroups(List<List<int>> splits, int groupCount)
    {
        // Find the smallest groups where the remaining packages can 
        // also be split into balanced groups of the same weight
        var i = 0;
        var targetCount = int.MaxValue;
        var minimalGroups = new List<List<int>>();
        var orderedSplits = splits.OrderBy(l => l.Count).ToList();
        while (i < orderedSplits.Count)
        {
            var currentSplit = orderedSplits[i];
            if (currentSplit.Count > targetCount)
            {
                // We already know the size of the smalles valid group
                // so we can skip processing the rest of the groups which
                // are larger
                break;
            }

            if (IsBalancedSplitPossible(currentSplit, orderedSplits, groupCount))
            {
                targetCount = currentSplit.Count;
                minimalGroups.Add(currentSplit);
            }
            i++;
        }
        return minimalGroups;
    }


    public static bool IsBalancedSplitPossible(List<int> split, List<List<int>> allSplits, int groupCount)
    {
        if (groupCount == 3)
        {
            return IsBalancedSplitToThreePossible(split, allSplits);
        }
        if (groupCount == 4)
        {
            return IsBalancedSplitToFourPossible(split, allSplits);
        }

        throw new Exception("Unsupported group count: " + groupCount);
    }

    private static bool IsBalancedSplitToThreePossible(List<int> split, List<List<int>> allSplits)
    {
        // Split is possible if we can find another split whose packages are not in the current split
        // We don't need to check for the third group since if group A + group B = 2/3 of the total weight
        // then the weight of the remaining packages must be 1/3 of the total weight
        return allSplits.Any(other => !other.Any(i => split.Contains(i)));
    }

    private static bool IsBalancedSplitToFourPossible(List<int> first, List<List<int>> allSplits)
    {
        return allSplits.Any(second =>
            !second.Any(i => first.Contains(i)) &&
            allSplits.Any(third =>
                !third.Any(i => first.Contains(i)) &&
                !third.Any(i => second.Contains(i))
            )
        );
    }

    public static long GetMinimalQuantumEntanglement(List<List<int>> minimalGroups, List<int> packages)
    {
        // Convert indexes to actual package weights and calculate the quantum entanglement
        return minimalGroups
            .Select(l => l.Select(i => packages[i]))
            .Select(l => l.Aggregate(1L, (a, b) => a * b))
            .OrderBy(qe => qe)
            .FirstOrDefault();
    }
}
