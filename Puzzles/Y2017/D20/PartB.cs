using System.Numerics;
using System.Text.RegularExpressions;
using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D20;

[PuzzleInfo(year: 2017, day: 20, part: 2, title: "Particle Swarm")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        var particles = ParseInput();
        var iteration = 0;
        while (iteration < 500)
        {
            iteration++;
            var updatedParticles = new List<Particle>();
            foreach (var p in particles)
            {
                var v = p.Velocity + p.Acceleration;
                var pos = p.Position + v;
                updatedParticles.Add(p with { Velocity = v, Position = pos });
            }

            var collided = new HashSet<Particle>();
            for (var i = 0; i < updatedParticles.Count; i++)
            {
                var a = updatedParticles[i];
                for (var j = i + 1; j < particles.Count; j++)
                {
                    var b = updatedParticles[j];
                    var distVec = a.Position - b.Position;
                    if (distVec.X == 0 && distVec.Y == 0 && distVec.Z == 0)
                    {
                        collided.Add(a);
                        collided.Add(b);
                        continue;
                    }
                }
            }
            particles = updatedParticles.Except(collided).ToList();
        }

        return particles.Count.ToString();
    }

    private List<Particle> ParseInput()
    {
        var re = new Regex("<(?<x>-?\\d+),(?<y>-?\\d+),(?<z>-?\\d+)>");
        var particles = Input.AsLines()
            .Select((line, index) =>
            {
                var matches = re.Matches(line);
                if (matches.Count != 3)
                    throw new InvalidOperationException($"Line is not valid: '{line}'");

                return new Particle(
                    index,
                    new Vector3DInt(
                        int.Parse(matches[0].Groups["x"].Value),
                        int.Parse(matches[0].Groups["y"].Value),
                        int.Parse(matches[0].Groups["z"].Value)
                    ),
                    new Vector3DInt(
                        int.Parse(matches[1].Groups["x"].Value),
                        int.Parse(matches[1].Groups["y"].Value),
                        int.Parse(matches[1].Groups["z"].Value)
                    ),
                    new Vector3DInt(
                        int.Parse(matches[2].Groups["x"].Value),
                        int.Parse(matches[2].Groups["y"].Value),
                        int.Parse(matches[2].Groups["z"].Value)
                    )
                );
            })
            .ToList();
        return particles;
    }
}

public record Particle(int Id, Vector3DInt Position, Vector3DInt Velocity, Vector3DInt Acceleration);
