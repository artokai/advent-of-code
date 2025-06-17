namespace Artokai.AOC.Puzzles.Y2015.D15;

public class CookieFactory
{
    private readonly List<Ingredient> _ingredients;

    public CookieFactory(List<Ingredient> ingredients)
    {
        _ingredients = ingredients;
    }

    public IEnumerable<List<int>> GetMixtures(int n, int total)
    {
        // Only one ingredient left, return the total amount
        if (n == 1)
        {
            yield return new List<int> { total };
            yield break;
        }

        for (var amount = 0; amount <= total; amount++)
        {
            int leftAmount = total - amount;
            foreach (var rest in GetMixtures(n - 1, leftAmount))
            {
                var result = new List<int> { amount };
                result.AddRange(rest);
                yield return result;
            }
        }
    }

    public int CalculateScore(List<int> mixture)
    {
        var capacity = 0;
        var durability = 0;
        var flavor = 0;
        var texture = 0;

        for (var i = 0; i < mixture.Count; i++)
        {
            var ingredient = _ingredients[i];
            var amount = mixture[i];
            capacity += ingredient.Capacity * amount;
            durability += ingredient.Durability * amount;
            flavor += ingredient.Flavor * amount;
            texture += ingredient.Texture * amount;
        }

        return Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) * Math.Max(0, texture);
    }

    internal int GetCalories(List<int> mixture)
    {
        var calories = 0;
        for (var i = 0; i < mixture.Count; i++)
        {
            calories += _ingredients[i].Calories * mixture[i];
        }
        return calories;
    }
}
