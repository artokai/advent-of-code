namespace Artokai.AOC.Puzzles.Y2024.D14;

public static class Simulation
{
    public static readonly int MAP_WIDTH = 101;
    public static readonly int MAP_HEIGHT = 103;

    public static List<Robot> MoveRobots(List<Robot> initial, int iterations)
    {
        var robots = new List<Robot>(initial);
        for (int i = 0; i < robots.Count; i++)
        {
            Robot r = robots[i];
            r.x = (r.x + r.dx * iterations) % MAP_WIDTH;
            if (r.x < 0)
            {
                r.x += MAP_WIDTH;
            }

            r.y = (r.y + r.dy * iterations) % MAP_HEIGHT;
            if (r.y < 0)
            {
                r.y += MAP_HEIGHT;
            }
            robots[i] = r;
        }
        return robots;
    }

    public static void PrintMap(List<Robot> robots)
    {
        for (var y = 0; y < MAP_WIDTH; y++)
        {
            for (var x = 0; x < MAP_HEIGHT; x++)
            {
                var cnt = robots.Where(r => r.x == x && r.y == y).Count();
                Console.Write(cnt <= 0 ? "." : cnt);
            }
            Console.WriteLine();
        }
    }
}
