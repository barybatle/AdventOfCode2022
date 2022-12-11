//Not the cleanest solution but fuck it

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

var cycleRegistryDict = new Dictionary<int, int>();

var registry = 1;
var cycle = 0;

foreach (var command in lines)
{
    if (command == "noop")
    {
        cycle ++;
        cycleRegistryDict.Add(cycle, registry);
        continue;
    }
    
    cycle+=2;
    cycleRegistryDict.Add(cycle-1, registry);
    cycleRegistryDict.Add(cycle, registry);

    registry += GetAddxValue(command);
}

var resultPart1 = cycleRegistryDict
    .Where(x => (x.Key + 20) % 40 == 0)
    .Select(x => x.Key * x.Value)
    .Sum();

Console.WriteLine($"Part 1 result: {resultPart1}");

var iterator = 1;

for (int i = 0; i < 6; i++)
{
    for (int j = 0; j < 40; j++)
    {
        var currentRegistry = cycleRegistryDict[iterator];
        iterator++;
        var spritePosition = Enumerable.Range(currentRegistry-1, 3);
        if (spritePosition.Any(x => x == j))
        {
            Console.Write('▓');
            continue;
        }
        Console.Write('.');
    }
    Console.WriteLine();
}

int GetAddxValue(string command)
{
    return int.Parse(command.Split(' ').Last());
}