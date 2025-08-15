using Artokai.AOC.Core;
using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2016.D22;

[PuzzleInfo(year: 2016, day: 22, part: 2, title: "Grid Computing")]
public class PartB : SolverBase
{
    public override string Solve()
    {
        // It's a maze where there is only one empty disc. Some of the drives are 
        // so big that they don't fit on our empty disc. Those are our walls.
        //
        // Solution:
        // 1. Move the empty disc to the left side of our target drive 
        //    Cost = 1 copy per move
        // 2. Move our target data to the top-left code
        //    Cost = 1 + 4 per move (4 operations to move the empty disc to the front again)
        //           The last move does not require the 4 extra steps
        //
        // By visualizing our map, we can also see that there is only one horizontal wall
        // So we directly can see the shortest path for step 1 visually:
        //   1. Move left n times until x = 1
        //   2. Move up n times until y = 0
        //   3. Move right n timess until x = xmax-2

        var drives = InputParser.ParseInput(Input).ToList();
        var (map, location) = CreateMap(drives);
        // map.Print(location);

        var step1count =
            (location.X - 1) + // Move left to x = 1
            (location.Y) +     // Move up to y = 0
            (map.Width - 3);   // Move right to x = xMax-2

        var step2count = (map.Width - 2) * 5 + 1;

        return (step1count + step2count).ToString();
    }

    private (Map, Vector2DInt) CreateMap(List<Drive> drives)
    {
        var maxX = drives.Max(d => d.X);
        var maxY = drives.Max(d => d.Y);
        var map = new Map(maxX + 1, maxY + 1);

        var emptyDrive = drives.First(d => d.Used == 0);
        var location = new Vector2DInt(emptyDrive.X, emptyDrive.Y);
        var wallDrives = drives.Where(d => d.Used > emptyDrive.Size);
        foreach (var wall in wallDrives)
        {
            map.SetTile(wall.X, wall.Y, true);
        }

        return (map, location);
    }
}
