namespace Artokai.AOC.Puzzles.Y2017.D13;

public record Layer(int Depth, int Range) {

    public static Layer Parse(string input)
    {
        var parts = input.Split(':', StringSplitOptions.TrimEntries);
        return new Layer(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public bool IsAtTopWhenLeavingAt(int startTime)
    {
        var stepsTaken = startTime + Depth;
        var stepsInFullBackAndForth = 2 * (Range - 1);
        return stepsTaken % stepsInFullBackAndForth == 0;
    }
};
