namespace Day_12;

public class Climber
{
    public Grid Map { get; }
    public Position Start { get; }
    public Queue<Position> ClimbQueue { get; private set; }
    public int[,] Distances;

    public Climber(Grid grid, Position start)
    {
        Map = grid;
        Start = start;
        Distances = new int[Map.Rows, Map.Cols];
        
        for (int i = 0; i < Map.Rows; i++)
        {
            for (int j = 0; j < Map.Cols; j++)
            {
                Distances[i,j] = -1;
            }
        }

        Distances[Start.Row, Start.Col] = 0;
        ClimbQueue = new Queue<Position>();
        ClimbQueue.Enqueue(Start);
    }

    public int DistanceTo(Position p)
    {
        return Distances[p.Row, p.Col];
    }

    public bool IsClimbing()
    {
        return ClimbQueue.Any();
    }

    public Position Climb()
    {
        if (!ClimbQueue.Any())
        {
            Console.WriteLine("Not possible duh");
        }
        
        Position currentClimb = ClimbQueue.Dequeue();

        var adjacentList = Map.GetAdjacent(currentClimb);
        
        foreach (Position adjacent in adjacentList)
        {
            if (Distances[adjacent.Row, adjacent.Col] == -1)
            {
                Distances[adjacent.Row, adjacent.Col] = Distances[currentClimb.Row, currentClimb.Col] + 1;
                ClimbQueue.Enqueue(adjacent);
            }
        }
        
        return currentClimb;
    }
}