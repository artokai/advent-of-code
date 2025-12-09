using System.Diagnostics.CodeAnalysis;

namespace Artokai.AOC.Puzzles.Y2025.D09;

public class TwoWayDictionary<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    private readonly Dictionary<TKey, TValue> forward = new();
    private readonly Dictionary<TValue, TKey> reverse = new();

    public void Add(TKey key, TValue value)
    {
        if (forward.ContainsKey(key))
            throw new ArgumentException($"Key {key} already exists in the dictionary.");
        if (reverse.ContainsKey(value))
            throw new ArgumentException($"Value {value} already exists in the dictionary.");

        forward[key] = value;
        reverse[value] = key;
    }

    public int Count => forward.Count;

    public bool ContainsKey(TKey key) => forward.ContainsKey(key);
    public bool ContainsValue(TValue value) => reverse.ContainsKey(value);

    public TValue GetValue(TKey key) => forward[key];
    public TKey GetKey(TValue value) => reverse[value];

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => forward.TryGetValue(key, out value);

    public bool TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key) => reverse.TryGetValue(value, out key);

}