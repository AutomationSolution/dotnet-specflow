using AutomationFramework.Utilities.Attributes;
using Humanizer;

namespace AutomationFramework.Extensions;

public static class EnumExtensions
{
    public static IList<T>? GetAttributes<T>(this Enum value) where T : Attribute
    {
        var customAttributes = value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(typeof (T), false);
        return customAttributes.Length != 0 ? (IList<T>) customAttributes : default;
    }

    public static string GetLocator(this Enum value, string? key = null)
    {
        try
        {
            var attribute = value.GetAttributes<LocatorAttribute>();
            return attribute is null ? value.Humanize() : attribute.Single(x =>
            {
                if (x.Key is null)
                {
                    return key is null;
                }

                return x.Key.Equals(key);
            }).Locator;
        }
        catch (InvalidOperationException e)
        {
            if (e.Message is "Sequence contains no matching element")
            {
                return value.Humanize();
            }

            throw;
        }
    }
}
