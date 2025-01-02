using Artokai.AOC.Core.Geometry;

namespace Artokai.AOC.Puzzles.Y2024.D06;

public struct Guard
{
    public static readonly Vector2DInt UP = new Vector2DInt(0, -1);
    public static readonly Vector2DInt RIGHT = new Vector2DInt(1, 0);
    public static readonly Vector2DInt DOWN = new Vector2DInt(0, 1);
    public static readonly Vector2DInt LEFT = new Vector2DInt(-1, 0);


    public static readonly byte B_UP = 1;
    public static readonly byte B_RIGHT = 2;

    public static readonly byte B_DOWN = 4;

    public static readonly byte B_LEFT = 8;


    public Vector2DInt Position { get; set; }
    public byte DirectionByte { get; set; }
    public Vector2DInt DirectionVector { get; set; }

    public bool IsMoving { get; set; }

    public Guard(Vector2DInt position, byte directionByte)
    {
        Vector2DInt dirVec = UP;
        if (directionByte == B_RIGHT)
        {
            dirVec = RIGHT;
        }
        else if (directionByte == B_DOWN)
        {
            dirVec = DOWN;
        }
        else if (directionByte == B_LEFT)
        {
            dirVec = LEFT;
        }

        IsMoving = true;
        Position = position;
        DirectionByte = directionByte;
        DirectionVector = dirVec;
    }

    public Guard(int x, int y, char directionChar)
    {
        IsMoving = true;
        Position = new Vector2DInt(x, y);

        if (directionChar == '^')
        {
            DirectionByte = B_UP;
            DirectionVector = UP;
            return;
        }

        if (directionChar == 'v')
        {
            DirectionByte = B_DOWN;
            DirectionVector = DOWN;
            return;
        }

        if (directionChar == '<')
        {
            DirectionByte = B_LEFT;
            DirectionVector = LEFT;
            return;
        }

        if (directionChar == '>')
        {
            DirectionByte = B_RIGHT;
            DirectionVector = RIGHT;
            return;
        }
    }

    public void TurnRight()
    {
        if (DirectionByte == B_UP)
        {
            DirectionByte = B_RIGHT;
            DirectionVector = RIGHT;
            return;
        }

        if (DirectionByte == B_RIGHT)
        {
            DirectionByte = B_DOWN;
            DirectionVector = DOWN;
            return;
        }

        if (DirectionByte == B_DOWN)
        {
            DirectionByte = B_LEFT;
            DirectionVector = LEFT;
            return;
        }

        DirectionByte = B_UP;
        DirectionVector = UP;
    }

    public string GetPositionString()
    {
        return $"{Position.X},{Position.Y}";
    }

    public char GetDirectionChar()
    {
        return DirectionVector switch
        {
            { X: -1, Y: 0 } => '<',
            { X: 1, Y: 0 } => '>',
            { X: 0, Y: 1 } => 'v',
            { X: 0, Y: -1 } => '^',
            _ => throw new Exception("Invalid direction")
        };
    }

    public Guard Clone()
    {
        return new Guard(Position, DirectionByte);
    }

    public void Move()
    {
        Position += DirectionVector;
    }

    public void Stop()
    {
        IsMoving = false;
    }
}
