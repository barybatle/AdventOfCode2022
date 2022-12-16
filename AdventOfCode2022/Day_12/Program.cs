using Day_12;

//Made possible by https://www.youtube.com/@CaptainCoder

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n").Select(x => x.TrimEnd('\r'));
var input = lines.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToCharArray());

var charGrid = Helper.CreateRectangularArray(input.ToList());

var grid = new Grid(Helper.ConvertGrid(charGrid));

var coordOfStart = Helper.GetPointCoords('S', charGrid);
var coordOfEnd = Helper.GetPointCoords('E', charGrid);

var climberPart1 = new Climber(grid, coordOfStart);

var lowestPoints = Helper.GetLowestPoints(grid.HeightMap);

var part2results = new List<int>();

while (climberPart1.IsClimbing())
{
    Position current = climberPart1.Climb();

    if (current.Row == coordOfEnd.Row && current.Col == coordOfEnd.Col)
    {
        Console.WriteLine("Part 1: " + climberPart1.DistanceTo(current));
        break;
    }
}

foreach (var lowPoint in lowestPoints)
{
    var climberPart2 = new Climber(grid, lowPoint);
    
    while (climberPart2.IsClimbing())
    {
        Position current = climberPart2.Climb();

        if (current.Row == coordOfEnd.Row && current.Col == coordOfEnd.Col)
        {
            part2results.Add(climberPart2.DistanceTo(current));
            break;
        }
    }
}

Console.WriteLine("Part 2: " + part2results.Min());

Console.WriteLine();