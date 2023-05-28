using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Extensions.Configuration;

public static class ConfigurationBuilderExtensions
{
    private static readonly string ConfigurationResourcesPath = Path.Combine("Resources", "Configuration");

    public static IConfigurationBuilder AddJsonFileFromResourcesDirectory(this IConfigurationBuilder configurationBuilder, string fileName, bool optional = true, bool reloadOnChange = true)
    {
        return configurationBuilder.AddJsonFile(Path.Combine(ConfigurationResourcesPath, fileName), optional, reloadOnChange);
    }
    
    public static IConfigurationBuilder AddFilesFromDirectoryAsJsonToConfigurationBuilder(this IConfigurationBuilder configurationBuilder, string directory)
    {
        foreach (var filePath in Directory.GetFiles(directory))
        {
            configurationBuilder.AddJsonFile(filePath);
        }

        return configurationBuilder;
    }
}