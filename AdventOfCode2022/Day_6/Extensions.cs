namespace Day_6;

public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        return new ArraySegment<T>(array, offset, length)
            .ToArray();
    }
}