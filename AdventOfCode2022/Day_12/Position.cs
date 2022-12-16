namespace Day_12;

public record Position(int Row, int Col)
{
    public Position Up => new Position(Row - 1, Col);
    public Position Down => new Position(Row + 1, Col);
    public Position Right => new Position(Row, Col + 1);
    public Position Left => new Position(Row, Col - 1);

    public List<Position> Adjacent()
    {
        return new List<Position>() {Up, Right, Down, Left};
    }
}