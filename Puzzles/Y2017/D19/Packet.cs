using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2017.D19;

public class Packet
{
    public Vector2DInt Position { get; private set; }
    public Vector2DInt? Direction { get; private set; }
    public MapData Grid { get; init; }
    public List<POI> POIs { get; init; }
    public List<POI> Collected { get; private set; } = new List<POI>();
    public int StepsTaken { get; private set; } = 1;

    public Packet(Vector2DInt startPosition, MapData grid, List<POI> pois)
    {
        Position = startPosition;
        Direction = Vector2DInt.Down;
        Grid = grid;
        POIs = pois;

        // Single step needed to enter the grid
        StepsTaken = 1;
    }

    public void Traverse()
    {
        do
        {
            StepsTaken++;
            Position += Direction!.Value;
            var poi = POIs.Where(p => p.Location == Position).FirstOrDefault();
            if (poi != null) { Collected.Add(poi); }
            Direction = GetNextDirection(Grid, Position, Direction.Value);
        } while (Direction != null);
    }

    private Vector2DInt? GetNextDirection(MapData grid, Vector2DInt currentPosition, Vector2DInt direction)
    {
        if (grid.Grid[currentPosition.X + direction.X, currentPosition.Y + direction.Y])
            return direction;

        var left = direction.TurnLeft();
        if (grid.Grid[currentPosition.X + left.X, currentPosition.Y + left.Y])
            return left;

        var right = direction.TurnRight();
        if (grid.Grid[currentPosition.X + right.X, currentPosition.Y + right.Y])
            return right;

        return null;
    }
}
