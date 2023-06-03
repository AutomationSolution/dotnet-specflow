namespace AutomationWeb.Extensions;

public static class StringExtensions
{
    public static string RemoveSpaces(this string input)
    {
        return input.Replace(" ", string.Empty);
    }
}
