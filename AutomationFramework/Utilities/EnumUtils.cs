namespace AutomationFramework.Utilities;

public static class EnumUtils
{
    private static readonly Random Random = new();

    public static T GetRandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        var result = values.GetValue(Random.Next(values.Length));
        if (result is not null) return (T) result;

        throw new InvalidOperationException($"Cannot get random enum value from {typeof(T)} type");
    }
}
