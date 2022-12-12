using System.Data;
using System.Text.RegularExpressions;

namespace Day_11;
//I simply "find all and replace" int to long so it isnt the most optimal solution
public class Monke
{
    public long Number { get; set; }
    public List<long> Items { get; set; }
    public string Operation { get; set; }
    public long DivisibleByTest { get; set; }
    public long TestPassed { get; set; }
    public long TestFailed { get; set; }
    public long ItemsInspected { get; set; }

    public Monke(string[] inputLines)
    {
        Number = long.Parse(inputLines[0].Split(' ').Last().TrimEnd(':'));
        Items = GetMonkeItems(inputLines[1].Split(':').Last());
        Operation = inputLines[2].Split(':').Last().Split('=').Last().Trim();
        DivisibleByTest = long.Parse(inputLines[3].Split(' ').Last());
        TestPassed = GetMonkeTest(inputLines[4]);
        TestFailed = GetMonkeTest(inputLines[5]);
    }

    private List<long> GetMonkeItems(string inputLine)
    {
        return inputLine.Split(',').Select(long.Parse).ToList();
    }

    private long GetMonkeTest(string inputLine)
    {
        return long.Parse(inputLine.Split(' ').Last());
    }

    public Dictionary<long, List<long>> InspectItems(long divideWorryLevelBy, bool isPart2, long part2Divider)
    {
        var itemsToRemove = new List<long>();
        var itemsToTransferDict = new Dictionary<long, List<long>>();

        foreach (var item in Items)
        {
            ItemsInspected++;
            itemsToRemove.Add(item);
            long nextMonke;

            var itemNewValue = Convert.ToInt64(Math.Floor((double) ExecuteOperation(item) / divideWorryLevelBy));
            
            if (isPart2)
            {
                itemNewValue = ExecuteOperation(item) % part2Divider;
            }
            
            nextMonke = TestFailed;

            if (itemNewValue % DivisibleByTest == 0)
            {
                nextMonke = TestPassed;
            }

            if (itemsToTransferDict.ContainsKey(nextMonke))
            {
                itemsToTransferDict[nextMonke].Add(itemNewValue);
                continue;
            }

            itemsToTransferDict.Add(nextMonke, new List<long> {itemNewValue});
        }

        itemsToRemove.ForEach(x => Items.Remove(x));

        return itemsToTransferDict;
    }

    public static void TransferItems(List<Monke> monkesList, Dictionary<long, List<long>> itemsToTransfer)
    {
        foreach (var item in itemsToTransfer)
        {
            monkesList.FirstOrDefault(x => x.Number == item.Key)!.Items.AddRange(item.Value);
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine($"Monke {Number} inspected items {ItemsInspected} times.");
    }

    private long ExecuteOperation(long value)
    {
        DataTable dt = new DataTable();

        var operationString = Operation.Replace("old", value.ToString());
        
        operationString = Regex.Replace(
            operationString, 
            @"\d+(\.\d+)?", 
            m => {
                var x = m.ToString(); 
                return x.Contains(".") ? x : string.Format("{0}.0", x);
            }
        );
        
        var operation = dt.Compute(operationString,"");

        return Convert.ToInt64(operation);
    }
}