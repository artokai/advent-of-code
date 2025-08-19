namespace Artokai.AOC.Puzzles.Y2017.D09;

public class Parser(string line)
{
    public int GarbageCharCount { get;  private set; } = 0;

    public IEnumerable<int> Parse()
    {
        GarbageCharCount = 0;

        int groupLevel = 0;
        var skipNext = false;
        var inGarbage = false;
        foreach (var c in line)
        {
            if (skipNext)
            {
                skipNext = false;
                continue;
            }

            if (c == '!')
            {
                skipNext = true;
                continue;
            }

            if (c == '>' && inGarbage)
            {
                inGarbage = false;
                continue;
            }

            if (inGarbage)
            {
                GarbageCharCount++;
                continue;
            }

            if (c == '<')
            {
                inGarbage = true;
                continue;
            }

            if (c == '{')
            {
                groupLevel++;
                continue;
            }

            if (c == '}')
            {
                yield return groupLevel;
                groupLevel--;
                continue;
            }
        }
    }
}
