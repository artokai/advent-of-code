
using Artokai.AOC.Core.Combinatorics;

namespace Artokai.AOC.Core.Math;

public static class NumberTheory
{
    public static IEnumerable<int> GetAllFactors(int n)
    {
        var primeFactors = GetPrimeFactors(n).Where(factor => factor != n);
        var powerset = primeFactors.GetPowerset();
        var factors = new HashSet<int>();
        foreach (var set in powerset)
        {
            // Note: Powerset includes an empty set, which will result in the factor 1
            var factor = set.Aggregate(1, (acc, val) => acc * val);
            factors.Add(factor);
        }
        factors.Add(n);
        return factors;
    }

    public static List<int> GetPrimeFactors(int n)
    {
        // https://en.wikipedia.org/wiki/Wheel_factorization
        var factors = new List<int>();

        // Divide by 2
        while (n % 2 == 0)
        {
            factors.Add(2);
            n /= 2;
        }

        // Divide by 3
        while (n % 3 == 0)
        {
            factors.Add(3);
            n /= 3;
        }

        // Divide by 5
        while (n % 5 == 0)
        {
            factors.Add(5);
            n /= 5;
        }

        // Handle larger factors
        var increments = new int[] { 4, 2, 4, 2, 4, 6, 2, 6 };
        var k = 7;
        var i = 0;
        while (k * k <= n)
        {
            if (n % k == 0)
            {
                factors.Add(k);
                n /= k;
            }
            else
            {
                k += increments[i];
                if (i < 7) i++; else i = 0;
            }
        }

        // If n is still greater than 1, it is a prime factor
        if (n > 1)
        {
            factors.Add(n);
        }

        return factors;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)System.Math.Floor(System.Math.Sqrt(number));

        for (var i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }
}
