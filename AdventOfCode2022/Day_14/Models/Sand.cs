namespace Day_14.Models;

public record Sand(Position Position, Cave Cave)
{
    public Position Position { get; private set; } = Position;

    public bool Fall()
    {
        if (!Cave.IsOccupied(Position.Down))
        {
            Position = Position.Down;
            return true;
        }
        
        if (!Cave.IsOccupied(Position.DownLeft))
        {
            Position = Position.DownLeft;
            return true;
        }
        
        if (!Cave.IsOccupied(Position.DownRight))
        {
            Position = Position.DownRight;
            return true;
        }
        
        return false;
    }
}