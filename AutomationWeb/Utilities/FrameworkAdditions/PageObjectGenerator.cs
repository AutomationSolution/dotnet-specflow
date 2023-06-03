using System.Reflection;
using AutomationWeb.Extensions;

namespace AutomationWeb.Utilities.FrameworkAdditions;

public static class PageObjectGenerator
{
    private const string PagesDirectory = "Pages";

    public static T GetPageObjectByName<T>(string pageObjectName)
    {
        var assemblyTypes = Assembly.GetCallingAssembly().GetTypes();
        var assemblyName = Assembly.GetCallingAssembly().GetName().Name;
        var basePagesDirectory = string.Join(".", assemblyName, PagesDirectory);

        foreach (var type in assemblyTypes)
        {
            if (type.FullName != null && type.FullName.StartsWith(basePagesDirectory) && type.Name == pageObjectName.RemoveSpaces())
            {
                var result = Activator.CreateInstance(type);
                if (result != null)
                {
                    return (T) result;
                }

                throw new InvalidOperationException($"Found type {type}, but unable to create an instance for it.");
            }
        }

        throw new InvalidOperationException($"Could not find a Page Object by name {pageObjectName}.");
    }
}
