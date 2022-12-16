using Day_13.Models;

namespace Day_13;

public class PacketComparer : IComparer<MessagePacket>
{
    public const int RightOrder = -1;
    public const int WrongOrder = 1;
    public const int TiedOrder = 0;
    
    public int Compare(MessagePacket? left, MessagePacket? right)
    {
        if (left is null)
        {
            throw new ArgumentException("Unexpected comparison target type.", nameof(left));
        }
        
        if (right is null)
        {
            throw new ArgumentException("Unexpected comparison target type.", nameof(right));
        }

        return ComparePackets(left, right);
    }

    private static int ComparePackets(MessagePacket left, MessagePacket right)
    {
        // Both values are numbers, so compare the packets' values.
        if (left is MessagePacketNumber leftNumber && right is MessagePacketNumber rightNumber)
        {
            return leftNumber.Number.CompareTo(rightNumber.Number);
        }
        
        // From this point on, AT LEAST ONE of the two packets is a list.
        var leftList = ConvertToList(left);
        var rightList = ConvertToList(right);
        var lowerBoundLimit = Math.Min(leftList.Packets.Length, rightList.Packets.Length);

        // Both lists were empty, there's nothing to compare against.
        if (leftList.Packets.Length == rightList.Packets.Length && lowerBoundLimit == 0)
        {
            return TiedOrder;
        }
        
        // From this point on, both lists have numbers in them, so let's compare each number.
        for (var i = 0; i < lowerBoundLimit; ++i)
        {
            var comparisonResult = ComparePackets(leftList.Packets[i], rightList.Packets[i]);
            if (comparisonResult != 0)
            {
                return comparisonResult;
            }
        }

        // We've reached the end of the shortest list. Comparing both lists' length will return 0 if both had the same length.
        return leftList.Packets.Length.CompareTo(rightList.Packets.Length);
    }

    private static MessagePacketList ConvertToList(MessagePacket input) =>
        input switch
        {
            MessagePacketList listPacket => listPacket,
            MessagePacketNumber numberPacket => new MessagePacketList(new MessagePacket[] { numberPacket }),
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Unsupported input type")
        };
}