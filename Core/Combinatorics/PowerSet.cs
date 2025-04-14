public static class PowerSetExtensions
{
    public static IEnumerable<IEnumerable<T>> GetPowerset<T>(this IEnumerable<T> set)
    {
        var list = set.ToList();
        int count = list.Count;
        int totalSubsets = 1 << count; // 2^count subsets

        for (int subsetMask = 0; subsetMask < totalSubsets; subsetMask++)
        {
            var subset = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                if ((subsetMask & (1 << i)) != 0)
                {
                    subset.Add(list[i]);
                }
            }
            yield return subset;
        }
    }
}
