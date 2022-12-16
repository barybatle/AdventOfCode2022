namespace Day_14.Models;

public record Cave(HashSet<Position> Rocks, int BottomY)
{
    public static readonly Position Origin = new(500, 0);
    public HashSet<Position> SettledSand { get; } = new();

    public static Cave Parse(string [] lines)
    {
        HashSet<Position> occupied = new();
        
        var bottomY = 0;
        
        foreach (var line in lines)
        {
            var positions = line.Split(" -> ");

            for (var i = 0; i < positions.Length - 1; i++)
            {

                var start = Position.Parse(positions[i]);
                var end = Position.Parse(positions[i + 1]);

                bottomY = Math.Max(bottomY, start.Y);
                bottomY = Math.Max(bottomY, end.Y);

                var segment = Position.BuildSegment(start, end);
                occupied.UnionWith(segment);
            }
        }
        
        return new Cave(occupied, bottomY);
    }

    public string PrintWindow(Position TopLeft, Position BottomRight)
    {
        string cave = string.Empty;

        for (int y = TopLeft.Y; y <= BottomRight.Y; y++)
        {
            for (int x = TopLeft.X; x <= BottomRight.X; x++)
            {
                var p = new Position(x, y);
                cave += PositionToSymbol(p);
            }

            cave += "\n";
        }
        return cave.Trim();
    }

    private char PositionToSymbol(Position p)
    {
        if (SettledSand.Contains(p))
        {
            return 'o';
        }
        if (Rocks.Contains(p))
        {
            return '#';
        }
        if (p == Origin)
        {
            return '+';
        }

        return '.';
    }

    public bool IsOccupied(Position p)
    {
        if (p.Y == BottomY + 2) //Part2
        {
            return true;
        }
        
        return Rocks.Contains(p) || SettledSand.Contains(p);
    }

    public bool DropSand()
    {
        Sand s = new Sand(Origin, this);
        
        while (s.Fall())
        {
            if (IsFinished(s.Position))
            {
                return false;
            }
        }

        SettledSand.Add(s.Position);
        return true;
    }

    public bool IsFinished(Position p)
    {

        //return p.Y >= BottomY; //Part1
        return p.Y == BottomY + 2;
    }
}