namespace Artokai.AOC.Core.Combinatorics;

public static class CombinationListExtensions {

    public static IEnumerable<IList<T>> GetCombinations<T>(this IList<T> list, int length)
    {
        return GetCombinations(list, 0, length);
    }

    private static IEnumerable<IList<T>> GetCombinations<T>(IList<T> list, int startIndex, int length)
    {
        if (length == 0)
        {
            yield return new List<T>();
            yield break;
        }

        if (startIndex + length > list.Count)
        {
            yield break;
        }

        // Return all combinations starting with the first element
        foreach (var subCombination in GetCombinations(list, startIndex + 1, length - 1))
        {
            yield return new List<T> { list[startIndex] }.Concat(subCombination).ToList();
        }

        // Return all combinations that don't start with the first element
        foreach (var combination in GetCombinations(list, startIndex + 1, length))
        {
            yield return combination;
        }
    }
}