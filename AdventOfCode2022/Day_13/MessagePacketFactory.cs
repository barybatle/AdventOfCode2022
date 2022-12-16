using System.Text.Json;
using Day_13.Models;

namespace Day_13;

public class MessagePacketFactory
{
    public List<(MessagePacket First, MessagePacket Second)> PairsFromInput(string input)
    {
        var pairs = input
            .Trim()
            .Split("\n\n")
            .Select(pair => pair.Split('\n'));

        return pairs
            .Select(segments => (
                First: FromJsonString(segments[0]),
                Second: FromJsonString(segments[1])
            ))
            .ToList();
    }

    public List<MessagePacket> ListFromInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new List<MessagePacket>();
        }

        var noBlankLines = input.Trim().Replace("\n\n", "\n").Split('\n');
        return noBlankLines.Select(FromJsonString).ToList();
    }

    private static MessagePacket FromJsonString(string json)
    {
        var element = (JsonElement)JsonSerializer.Deserialize<object>(json)!;
        return FromJsonElement(element);
    }

    private static MessagePacket FromJsonElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Number => new MessagePacketNumber(element.GetInt32()),
            JsonValueKind.Array => new MessagePacketList(element.EnumerateArray().Select(FromJsonElement).ToArray()),
            _ => throw new ArgumentException()
        };
    }
}