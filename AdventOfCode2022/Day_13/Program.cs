//stolen from https://github.com/micka190

using Day_13;

var input = File.ReadAllText("input.txt");
var factory = new MessagePacketFactory();
var comparer = new PacketComparer();
var solver = new Solver(factory, comparer);
Console.WriteLine($"PART 1 - {solver.SolveForPartOne(input)}");
Console.WriteLine($"PART 2 - {solver.SolveForPartTwo(input)}");