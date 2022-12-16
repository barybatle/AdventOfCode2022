namespace Day_12;

public static class Helper
{
    public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
    {
        // TODO: Validation and special-casing for arrays.Count == 0
        int minorLength = arrays[0].Length;
        T[,] ret = new T[arrays.Count, minorLength];
        for (int i = 0; i < arrays.Count; i++)
        {
            var array = arrays[i];
            if (array.Length != minorLength)
            {
                throw new ArgumentException
                    ("All arrays must be the same length");
            }
            for (int j = 0; j < minorLength; j++)
            {
                ret[i, j] = array[j];
            }
        }
        return ret;
    }

    public static Position GetPointCoords(char C, char[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == C)
                {
                    return new Position(i, j);
                }
            }
        }

        return new Position(-1, -1);
    }

    public static int[,] ConvertGrid(char[,] inputGrid)
    {
        int rows = inputGrid.GetLength(0);
        int cols = inputGrid.GetLength(1);
        
        var result = new int[rows,cols];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                switch (inputGrid[i, j])
                {
                    case 'S':
                        result[i, j] = 0;
                        continue;
                    case 'E':
                        result[i, j] = 'z' - 'a';
                        continue;
                    default:
                        result[i, j] = inputGrid[i, j] - 'a';
                        break;
                }
            }
        }

        return result;
    }

    public static List<Position> GetLowestPoints(int[,] inputGrid)
    {
        var result = new List<Position>();
        
        int rows = inputGrid.GetLength(0);
        int cols = inputGrid.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (inputGrid[i, j] == 0)
                {
                    result.Add(new Position(i, j));
                }
            }   
        }

        return result;
    }
}