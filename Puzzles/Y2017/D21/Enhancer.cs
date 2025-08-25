namespace Artokai.AOC.Puzzles.Y2017.D21;

public class Enhancer(Dictionary<int, Dictionary<string, Grid>> Patterns)
{
    public Grid Enhance(Grid input)
    {
        return (input.Size % 2 == 0)
            ? ProcessPatterns(input, 2, Patterns[2])
            : ProcessPatterns(input, 3, Patterns[3]);
    }

    private Grid ProcessPatterns(Grid input, int inputPatternSize, Dictionary<string, Grid> patternsForInputSize)
    {
        var patternMatchesCount = input.Size / inputPatternSize;
        var outputPatternSize = inputPatternSize + 1;
        var resultSize = patternMatchesCount * (inputPatternSize + 1);
        var result = new bool[resultSize, resultSize];

        for (var py = 0; py < patternMatchesCount; py++)
        {
            for (var px = 0; px < patternMatchesCount; px++)
            {
                var subGrid = input.ExtractSubGrid(px * inputPatternSize, py * inputPatternSize, inputPatternSize);
                var matchingPattern = patternsForInputSize[subGrid.ToString()];
                for (var dy = 0; dy < outputPatternSize; dy++)
                {
                    for (var dx = 0; dx < outputPatternSize; dx++)
                    {
                        var resultX = px * outputPatternSize + dx;
                        var resultY = py * outputPatternSize + dy;
                        result[resultX, resultY] = matchingPattern.Cells[dx, dy];
                    }
                }
            }
        }

        return new Grid(result, resultSize);
    }
}
