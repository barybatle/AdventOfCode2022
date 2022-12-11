var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

var shipment = GetInitialSetup(lines).ToList();
var instructions = GetSteps(lines).ToList();

instructions.ForEach(instruction => SwitchItems(instruction, shipment));

Console.WriteLine(shipment.Select(x => x.Last()).ToArray());

Console.ReadKey();

IEnumerable<string> GetInitialSetup(string[] lines)
{
    var initialSetup = lines.Take(8);
    
    var emptyList = new List<string> {"", "", "", "", "", "", "", "", ""};
    
    return initialSetup
        .Select(line => line
            .Chunk(4)
            .Select(x => new string(x).Trim(' ', '[', ']')))
        .Aggregate(emptyList, (current, test) => current
            .Zip(test, (first, second) => first + second)
            .ToList())
        .Select(x => new string(x.Reverse().ToArray())).ToList();
}

IEnumerable<string> GetSteps(string[] lines)
{
    return lines.Skip(9);
}

void SwitchItems(string line, List<string> shipment)
{
    (int howMany, int from, int to) = ExtractStep(line);

    //var taken = new string(shipment[from].TakeLast(howMany).Reverse().ToArray()); //PART1
    var taken = new string(shipment[from].TakeLast(howMany).ToArray()); //PART2
    shipment[from] = shipment[from].Remove(shipment[from].Length - howMany, howMany);

    shipment[to] = new string(shipment[to].Concat(taken).ToArray());
}

(int howMany, int from, int to) ExtractStep(string line)
{
    var lineSplit = line.Split(' ');
    int howMany = int.Parse(lineSplit[1]);
    int from = int.Parse(lineSplit[3]) - 1;
    int to = int.Parse(lineSplit[5]) - 1;

    return (howMany, from, to);
}