using Artokai.AOC.Core;

namespace Artokai.AOC.Puzzles.Y2017.D15;

[PuzzleInfo(year: 2017, day: 15, part: 1, title: "Dueling Generators")]
public class PartA : SolverBase
{
    public override string Solve()
    {
        var seeds = Input.AsLines()
            .Select(line => int.Parse(line.Split(' ').Last()))
            .ToArray();
        var generatorA = Generator.Generate(seeds[0], 16807).GetEnumerator();
        var generatorB = Generator.Generate(seeds[1], 48271).GetEnumerator();

        int counter = 0;
        for (var i = 0; i < 40_000_000; i++)
        {
            generatorA.MoveNext();
            generatorB.MoveNext();
            var aValue = generatorA.Current & 0xFFFF;
            var bValue = generatorB.Current & 0xFFFF;
            if (aValue == bValue)
            {
                counter++;
            }
        }

        return counter.ToString();
    }
}
