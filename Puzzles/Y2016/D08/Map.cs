using System.Text;

namespace Artokai.AOC.Puzzles.Y2016.D08;

public class Map {
    public int Width { get; }
    public int Height { get; }
    public bool[,] Pixels { get; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Pixels = new bool[width, height];
    }

    public override string ToString() {
        var output = new StringBuilder();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                output.Append(Pixels[x, y] ? "#" : " ");
            }
            output.AppendLine();
        }
        return output.ToString();
    }
}
