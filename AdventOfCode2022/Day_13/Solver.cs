using Day_13.Models;

namespace Day_13;

public class Solver
{
    private readonly MessagePacketFactory _factory;
    private readonly PacketComparer _comparer;

    public Solver(MessagePacketFactory factory, PacketComparer comparer)
    {
        _factory = factory;
        _comparer = comparer;
    }

    public int SolveForPartOne(string input)
    {
        var pairs = _factory.PairsFromInput(input);
        return pairs
            .Select((pair, index) => (Order: _comparer.Compare(pair.First, pair.Second), Index: index + 1)) // "+ 1" for 1-based indexing
            .Where(data => data.Order == PacketComparer.RightOrder)
            .Sum(data => data.Index);
    }

    public int SolveForPartTwo(string input)
    {
        var firstDividerPacket = new MessagePacketList(new MessagePacket[] { new MessagePacketNumber(2) });
        var secondDividerPacket = new MessagePacketList(new MessagePacket[] { new MessagePacketNumber(6) });
        
        var packets = _factory.ListFromInput(input);
        packets.Add(firstDividerPacket);
        packets.Add(secondDividerPacket);
        packets.Sort(_comparer);

        var firstDividerIndex = packets.FindIndex(packet => packet == firstDividerPacket) + 1; // "+ 1" for 1-based indexing
        var secondDividerIndex = packets.FindIndex(packet => packet == secondDividerPacket) + 1; // "+ 1" for 1-based indexing

        return firstDividerIndex * secondDividerIndex;
    }
}