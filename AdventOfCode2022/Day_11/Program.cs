using Day_11;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n").Select(x => x.TrimEnd('\r')).Select(x => x.Trim());
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

var monkeLines = lines.Chunk(6);

var monkes = monkeLines
    .Select(monkeLine => new Monke(monkeLine)).
    ToList();

var roundCountPart1 = 20;
var roundCountPart2 = 10000;
int divideByWorryLevel1 = 3;
int divideByWorryLevel2 = 1;
int howManyMonkesToTake = 2;

var part2Divider = monkes.Select(x => x.DivisibleByTest).Aggregate(1L, (current, item) => current * item);

ExecuteRounds(roundCountPart1, divideByWorryLevel1);
//ExecuteRounds(roundCountPart2, divideByWorryLevel2, true, part2Divider);

monkes.ForEach(x => x.DisplayStats());

var result = monkes
    .OrderByDescending(x => x.ItemsInspected)
    .Take(howManyMonkesToTake)
    .Select(x => x.ItemsInspected)
    .Aggregate(1L, (current, item) => current * item);

Console.WriteLine($"Result: {result}");

void ExecuteRounds(int roundCount, int divideByWorryLevel, bool isPart2 = false, long part2Divider = 1)
{
    for (int i = 0; i < roundCount; i++)
    {
        //Console.WriteLine($"Round {i+1}");
        foreach (var monke in monkes)
        {
            //Console.WriteLine($"Monke {monke.Number} items: {string.Join(", ", monke.Items)}");
            var itemsToTransfer = monke.InspectItems(divideByWorryLevel, isPart2, part2Divider);
            Monke.TransferItems(monkes, itemsToTransfer);
        }
    }
}
