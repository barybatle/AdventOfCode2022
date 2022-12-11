var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

//Part 1

int resultPart1 = 0;

foreach (var line in lines)
{
    var firstCompartment = line[..(line.Length / 2)].AsEnumerable();
    var secondCompartment = line.Substring(line.Length / 2).AsEnumerable();

    var mixedItems = firstCompartment.Intersect(secondCompartment);

    foreach (var mixedItem in mixedItems)
    {
        resultPart1 += GetPriority(mixedItem);
    }
}

Console.WriteLine(resultPart1);

//Part 2

int resultPart2 = 0;

var elfGroups = lines.Chunk(3);

foreach (var elfGroup in elfGroups)
{
    var groupBadge = GetGroupBadge(elfGroup);

    resultPart2 += GetPriority(groupBadge);
}

Console.WriteLine(resultPart2);

int GetPriority(char character)
{
    int priority = character % 32;

    if (char.IsUpper(character))
    {
        priority += 26;
    }
    
    return priority;
}

char GetGroupBadge(string[] elfGroups)
{
    var badge = elfGroups[0].Intersect(elfGroups[1].Intersect(elfGroups[2])).FirstOrDefault();

    return badge;
}