namespace AutomationFramework.Utilities;

public static class EnumUtils
{
    private static readonly Random Random = new();

    public static T GetRandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T) values.GetValue(Random.Next(values.Length));
    }
}