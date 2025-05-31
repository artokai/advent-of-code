using System.Security.Cryptography;

namespace Artokai.AOC.Puzzles.Y2016.D14;

public class Solver
{
    private readonly string _salt;
    private readonly int _lookAheadCount;
    private readonly int _stretchCount;

    public Solver(string salt, int lookAheadCount, int strechCount = 0)
    {
        _salt = salt;
        _lookAheadCount = lookAheadCount;
        _stretchCount = strechCount;
    }

    public int Solve(int targetKeyNo)
    {
        var index = 0;
        var keyNo = 0;
        var memo = new Dictionary<string, HashInfo>();
        while (keyNo < targetKeyNo)
        {
            index++;

            var currentHashInfo = GenerateHashInfo(memo, _salt, index);
            if (!currentHashInfo.HasTriplets())
                continue;

            var tripletChar = currentHashInfo.GetFirstTriplet();
            var futureHasMatchingQuintuplets = LookAhead(
                memo,
                _salt,
                index + 1,
                _lookAheadCount,
                (future) => future.Quintuplets.Any(c => c == tripletChar)
            );
            if (futureHasMatchingQuintuplets)
            {
                // Found a key!
                keyNo++;
            }
        }

        return index;
    }

    private string Hash(string input, int repeatCount = 0)
    {
        var current = input;
        for (int i = 0; i <= repeatCount; i++)
        {
            var hashBytes = MD5.HashData(System.Text.Encoding.UTF8.GetBytes(current));
            current = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        return current;
    }

    private HashInfo GenerateHashInfo(Dictionary<string, HashInfo> memo, string salt, int index)
    {
        var stringToHash = salt + index.ToString();
        if (memo.ContainsKey(stringToHash))
            return memo[stringToHash];

        var hash = Hash(stringToHash, _stretchCount);
        var triplets = new HashSet<char>();
        var quintuplets = new HashSet<char>();
        for (int i = 0; i < hash.Length - 2; i++)
        {
            char c0 = hash[i];
            char c1 = hash[i + 1];
            char c2 = hash[i + 2];
            char c3 = i < hash.Length - 3 ? hash[i + 3] : '\0';
            char c4 = i < hash.Length - 4 ? hash[i + 4] : '\0';

            if (c0 == c1 && c0 == c2)
            {
                triplets.Add(c0);
                if (c0 == c3 && c0 == c4)
                {
                    quintuplets.Add(c0);
                }
            }
        }

        var hashInfo = new HashInfo(index, hash, triplets, quintuplets);
        memo[stringToHash] = hashInfo;
        return hashInfo;
    }

    private bool LookAhead(Dictionary<string, HashInfo> memo, string salt, int startIndex, int lookAheadCount, Func<HashInfo, bool> predicate)
    {
        for (int i = 0; i < lookAheadCount; i++)
        {
            var index = startIndex + i;
            var hashInfo = GenerateHashInfo(memo, salt, index);
            if (predicate(hashInfo))
                return true;
        }
        return false;
    }
}

public record struct HashInfo(int Index, string Hash, HashSet<char> Triplets, HashSet<char> Quintuplets)
{
    public char GetFirstTriplet() => Triplets.FirstOrDefault('\0');
    public bool HasTriplets() => Triplets.Count > 0;
    public bool HasQuintuplets() => Quintuplets.Count > 0;
}
