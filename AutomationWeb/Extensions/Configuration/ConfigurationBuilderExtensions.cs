using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Extensions.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddFilesFromDirectoryAsJsonToConfigurationBuilder(this IConfigurationBuilder configurationBuilder, string directory)
    {
        foreach (var filePath in Directory.GetFiles(directory))
        {
            configurationBuilder.AddJsonFile(filePath);
        }

        return configurationBuilder;
    }
}