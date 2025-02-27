namespace Artokai.AOC.Core.Combinatorics;

public enum PermutationMode
{
    Linear,
    Circular,
    CircularIgnoreDirection
}

public static class PermutationListExtensions
{
    public static IEnumerable<IList<T>> GetPermutations<T>(this IList<T> list, PermutationMode mode = PermutationMode.Linear)
    {
        int count = list.Count;
        var indices = new int[count];
        for (int i = 0; i < count; i++)
        {
            indices[i] = i;
        }

        yield return list.Select((item, index) => list[indices[index]]).ToList();

        while (true)
        {
            int left = -1;
            for (int i = count - 2; i >= (mode == PermutationMode.Linear ? 0 : 1); i--)
            {
                if (indices[i] < indices[i + 1])
                {
                    left = i;
                    break;
                }
            }

            if (left == -1)
            {
                yield break;
            }

            int right = -1;
            for (int i = count - 1; i > left; i--)
            {
                if (indices[left] < indices[i])
                {
                    right = i;
                    break;
                }
            }

            (indices[left], indices[right]) = (indices[right], indices[left]);
            Array.Reverse(indices, left + 1, count - (left + 1));

            if (mode != PermutationMode.CircularIgnoreDirection || indices[1] <= indices[count - 1])
            {
                yield return list.Select((item, index) => list[indices[index]]).ToList();
            }
        }
    }
}
