using System.Text.RegularExpressions;

namespace Artokai.AOC.Puzzles.Y2015.D14;

public class Reindeer
{
    public string Name { get; set; }
    public int Speed { get; set; }
    public int FlyTime { get; set; }
    public int RestTime { get; set; }

    public Reindeer(string line)
    {
        var re = new Regex(@"(?<Name>\w+) can fly (?<Speed>\d+) km/s for (?<FlyTime>\d+) seconds, but then must rest for (?<RestTime>\d+) seconds.");
        var match = re.Match(line);
        if (!match.Success)
        {
            throw new ArgumentException("Invalid input");
        }

        Name = match.Groups["Name"].Value;
        Speed = int.Parse(match.Groups["Speed"].Value);
        FlyTime = int.Parse(match.Groups["FlyTime"].Value);
        RestTime = int.Parse(match.Groups["RestTime"].Value);        
    }

    public int DistanceTravelledAt(int seconds)
    {
        var totalCycles = seconds / (FlyTime + RestTime);
        var remaining = seconds - (totalCycles * (FlyTime + RestTime));
        return totalCycles * Speed * FlyTime + Math.Min(FlyTime, remaining) * Speed;
    }    
}
