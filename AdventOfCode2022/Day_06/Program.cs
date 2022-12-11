using Day_6;

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Part 1 result: {PerformDecoding(input, 4)}");

Console.WriteLine($"Part 2 result: {PerformDecoding(input, 14)}");

int PerformDecoding(string inputString, int windowSize)
{
    var inputArray = inputString.ToArray();

    var result = 0;

    char[] subArray;

    do
    {
        subArray = inputArray.SubArray(result, windowSize);
        result++;
    } while (subArray.Distinct().Count() != windowSize);

    return result + windowSize - 1;
}