using System.Collections.Generic;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

var resultPart1 = lines
    .Select(x => new ElfPair(x))
    .Count(x => x.CheckIfElfsHaveTheSameArea(x));

var resultPart2 = lines
    .Select(x => new ElfPair(x))
    .Count(x => x.CheckIfElfsOverlap(x));

Console.WriteLine(resultPart1);
Console.WriteLine(resultPart2);

class ElfPair
{
    public IEnumerable<int> FirstElf { get; set; }
    public IEnumerable<int> SecondElf { get; set; }

    public ElfPair(string line)
    {
        var pair = line.Split(",");

        FirstElf = GetElfRange(pair[0]);
        SecondElf = GetElfRange(pair[1]);
    }

    List<int> GetElfRange(string elf)
    {
        var values = elf.Split("-").Select(int.Parse).ToArray();

        return Enumerable.Range(values[0], values[1] - values[0] + 1).ToList();
    }

    public bool CheckIfElfsHaveTheSameArea(ElfPair elfPair)
    {
        var firstElf = elfPair.FirstElf;
        var secondElf = elfPair.SecondElf;

        return (!firstElf.Except(secondElf).Any() || !secondElf.Except(firstElf).Any());
    }

    public bool CheckIfElfsOverlap(ElfPair elfPair)
    {
        var firstElf = elfPair.FirstElf;
        var secondElf = elfPair.SecondElf;

        return (firstElf.Intersect(secondElf).Any() || secondElf.Intersect(firstElf).Any());
    }
}