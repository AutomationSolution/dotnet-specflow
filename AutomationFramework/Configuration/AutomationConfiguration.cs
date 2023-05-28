using AutomationFramework.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutomationFramework.Configuration;

/// <summary>
/// Class to keep all configuration settings at one place for all dependent projects.
/// </summary>
public class AutomationConfiguration
{
    private static ConfigurationManager? ConfigurationManagerInstance;
    
    // Objects accessible within AutomationFramework project and all dependent projects
    // If needed, other projects can use it's own objects by binding them from Instance 
    public static RuntimeConfigurationModel RuntimeConfigurationModel { get; private set; }
    public static LoggingModel LoggingModel { get; private set; }

    public static ConfigurationManager Instance
    {
        get
        {
            if (ConfigurationManagerInstance is null)
            {
                ConfigurationManagerInstance = new ConfigurationManager();
                AddDefaultStaticSources();
            }

            return ConfigurationManagerInstance;
        }
    }
    
    private const string AppSettingsFileName = "appsettings.json";

    /// <summary>
    /// Sources required for all projects which will use this class as configuration
    /// </summary>
    private static void AddDefaultStaticSources()
    {
        ConfigurationManagerInstance.AddJsonFile(AppSettingsFileName);
        RuntimeConfigurationModel = ConfigurationManagerInstance.GetRequiredSection("RuntimeConfiguration").Get<RuntimeConfigurationModel>(); // Partial load to use correct environment.json file
    }
}