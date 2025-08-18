using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D04;

[PuzzleInfo(year: 2017, day: 4, part: 2, title: "High-Entropy Passphrases")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var passphrases = Input.AsLists<string>();
        var validCount = passphrases
            .Where(words => !ContainsAnagrams(words))
            .Count();
        return validCount.ToString();
    }

    private bool ContainsAnagrams(List<string> words)
    {
        for (var i = 0; i < words.Count - 1; i++)
        {
            for (var j = i + 1; j < words.Count; j++)
            {
                if (IsAnagram(words[i], words[j]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsAnagram(string word1, string word2)
    {
        if (word1.Length != word2.Length)
            return false;

        var word1Sorted = word1.ToCharArray().OrderBy(c => c).ToArray();
        var word2Sorted = word2.ToCharArray().OrderBy(c => c).ToArray();
        return word1Sorted.SequenceEqual(word2Sorted);
    }
}
