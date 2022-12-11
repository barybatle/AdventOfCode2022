//Part 1
/*
var result = File.ReadAllText("input.txt")
    .Split("\n\n")
    .Select(group => group.Split("\n")
        .Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse))
    .Select(x => x.Sum())
    .Max();
*/
//Part 2  

var result = File.ReadAllText("input.txt")
    .Split("\n\n")
    .Select(group => group.Split("\n")
        .Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse))
    .Select(x => x.Sum())
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();

Console.WriteLine(result);