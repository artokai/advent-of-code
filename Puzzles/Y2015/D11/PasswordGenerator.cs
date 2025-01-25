namespace Artokai.AOC.Puzzles.Y2015.D11;

public static class PasswordGenerator
{
    public static string Generate(string oldPassword)
    {
        var password = oldPassword.ToCharArray();
        Increment(password);
        while (!IsValidPassword(password))
        {
            Increment(password);
        }
        return new string(password);
    }

    private static bool IsValidPassword(char[] password)
    {
        var meetsConsecutiveRequirement = false;
        var meetsPairRequirement = false;

        var firstPairChar = '*';
        var consecutiveCount = 0;
        for (var i = 0; i < password.Length; i++)
        {
            var c = password[i];

            consecutiveCount = (i == 0 || password[i - 1] != c - 1) ? 1 : consecutiveCount + 1;
            if (consecutiveCount >= 3) { meetsConsecutiveRequirement = true; }

            if (i > 0 && c == password[i - 1])
            {
                if (firstPairChar == '*')
                {
                    firstPairChar = c;
                }
                else if (firstPairChar != c)
                {
                    meetsPairRequirement = true;
                }
            }
        }

        return meetsConsecutiveRequirement && meetsPairRequirement;
    }

    private static void Increment(char[] password)
    {
        var shouldIncrement = true;
        for (var i = password.Length - 1; i >= 0; i--)
        {
            var c = password[i];
            if (shouldIncrement)
            {
                password[i] = IncrementChar(c);
                shouldIncrement = password[i] == 'a';
            }
            else
            {
                password[i] = c;
            }
        }
    }

    private static char IncrementChar(char c)
    {
        if (c == 'z') return 'a';
        var newChar = Convert.ToChar(c + 1);
        if (newChar == 'i') newChar = 'j';
        if (newChar == 'o') newChar = 'p';
        if (newChar == 'l') newChar = 'm';
        return newChar;
    }
}
