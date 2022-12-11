using System.Xml.Xsl;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
var treeGrid = lines.Select(x => Array.ConvertAll(x.ToArray(), c => (int) Char.GetNumericValue(c))).ToList();

var result = lines.Length * 2 + lines.FirstOrDefault().Length * 2 - 4;

int highestScore = 0;

for (int i = 1; i < lines.Length - 1; i++)
{
    for (int j = 1; j < lines[i].Length - 1; j++)
    {
        var column = GetColumn(j, treeGrid);
        var row = treeGrid[i];

        var currentTree = treeGrid[i][j];
        
        if (CheckGrid(currentTree, column, i) || CheckGrid(currentTree, row, j))
        {
            result++;
        }

        int score = GetScore(column, row, i, j, currentTree);
        
        if (score > highestScore)
        {
            highestScore = score;
        }
    }
}

Console.WriteLine(result);
Console.WriteLine(highestScore);
Console.ReadKey();

bool CheckGrid(int currentTree, IEnumerable<int> row, int index)
{
    var (left, right) = SplitRow(row, index);

    return !left.Any(x => x >= currentTree) || !right.Any(x => x >= currentTree);
}

IEnumerable<int> GetColumn(int index, IEnumerable<int[]> grid)
{
    return grid.Select(x => x[index]);
}

(int[], int[]) SplitRow(IEnumerable<int> row, int index)
{
    var left = row.Take(index).ToArray();
    var right = row.Skip(index + 1).ToArray();
    
    return (left, right);
}

int GetScore(IEnumerable<int> column, IEnumerable<int> row, int columnIndex, int rowIndex, int currentTree)
{
    var (columnLeft, columnRight) = SplitRow(column, columnIndex);
    var (rowLeft, rowRight) = SplitRow(row, rowIndex);

    var columnLeftScore = GetParticularScore(columnLeft, true, currentTree);
    var columnRightScore = GetParticularScore(columnRight, false, currentTree);
    var rowLeftScore = GetParticularScore(rowLeft, true, currentTree);
    var rowRightScore = GetParticularScore(rowRight, false, currentTree);
    
    int score = columnLeftScore * columnRightScore * rowLeftScore * rowRightScore;

    return score;
}

int GetParticularScore(IEnumerable<int> line, bool isLeft, int value)
{
    int result = 0;
    
    if (isLeft)
    {
        line = line.Reverse();
    }

    if (line.Any(x => x == value))
    {
        result += 1;
    }

    return result + line.TakeWhile(x => x != value && x <= value).Count();
}