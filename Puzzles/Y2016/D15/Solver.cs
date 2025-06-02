using System.Text.RegularExpressions;
using Artokai.AOC.Core.Input;

namespace Artokai.AOC.Puzzles.Y2016.D15;

public record Disc(int Id, int Size, int InitialPosition);

public static class Solver
{
    public static List<Disc> ParseInput(PuzzleInput input)
    {
        var re = new Regex(@"Disc #(?<id>\d+) has (?<count>\d+) positions; at time=(?<time>\d+), it is at position (?<position>\d+).");
        var lines = input.AsLines();
        var discs = lines.Select(line =>
        {
            var match = re.Match(line);
            if (!match.Success)
                throw new InvalidOperationException($"Line '{line}' does not match the expected format.");
            var id = int.Parse(match.Groups["id"].Value);
            var positions = int.Parse(match.Groups["count"].Value);
            var initialPosition = int.Parse(match.Groups["position"].Value);
            return new Disc(id, positions, initialPosition);
        }).ToList();
        return discs;
    }

    public static int Solve(List<Disc> discs)
    {
        var sortedDiscs = discs.OrderByDescending(d => d.Size).ToList();
        var largest = sortedDiscs.First();

        // Calculate the first drop time when the largest disc is open.
        var firstDrop = (largest.Size - largest.InitialPosition) % largest.Size - largest.Id;

        // Brute force
        // - Start with the first drop time when the largest disc is open
        // - Advance the drop time by the size of the largest disc
        var dropTime = firstDrop;
        while (sortedDiscs.All(d => d.IsOpen(dropTime)) == false)
        {
            dropTime += largest.Size;
        }

        return dropTime;
    }

    private static bool IsOpen(this Disc disc, int dropTime)
    {
        var hitTime = dropTime + disc.Id;
        var position = disc.InitialPosition + hitTime;
        return position % disc.Size == 0;
    }
}
