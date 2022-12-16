namespace Day_12;

public record Grid(int[,] HeightMap)
{
    public int Rows => HeightMap.GetLength(0);
    public int Cols = HeightMap.GetLength(1);

    public List<Position> GetAdjacent(Position position)
    {
        var adjacentList = new List<Position>();

        foreach (var adjacent in position.Adjacent())
        {
            if (CanMove(position, adjacent))
            {
                adjacentList.Add(adjacent);
            }
        }

        return adjacentList;
    }
    
    public bool Contains(Position pos)
    {
        return    pos.Row >= 0 
               && pos.Col >= 0 
               && pos.Row < Rows 
               && pos.Col < Cols;
    }

    public bool CanMove(Position from, Position to)
    {
        if (!Contains(from) || !Contains(to))
        {
            return false;
        }
        
        var heightTo = HeightMap[to.Row, to.Col];
        var heightFrom = HeightMap[from.Row, from.Col];
        
        return heightTo <= heightFrom + 1;
    }
}