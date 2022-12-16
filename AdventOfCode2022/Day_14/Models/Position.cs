namespace Day_14.Models;

public record Position(int X, int Y)
{
    public Position Down => new Position(X, Y + 1);
    public Position DownLeft => new Position(X - 1, Y + 1);
    public Position DownRight => new Position(X + 1, Y + 1);

    public static Position Parse(string pos)
    {
        var split = pos.Split(',').Select(int.Parse).ToList();

        return new Position(split.First(), split.Last());
    }

    public static HashSet<Position> BuildSegment(Position start, Position end)
    {
        HashSet<Position> ps = new() {start};

        while (start != end)
        {
            var diffX = Math.Sign(end.X - start.X);
            var diffY = Math.Sign(end.Y - start.Y);

            start = new Position(start.X + diffX, start.Y + diffY);

            ps.Add(start);
        }

        return ps;
    }
}