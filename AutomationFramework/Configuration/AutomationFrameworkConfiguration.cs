using AutomationFramework.Models.Configuration;
using Microsoft.Extensions.Configuration;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationFramework.Configuration;

public class AutomationFrameworkConfiguration : IAutomationConfiguration
{
    // Objects accessible within AutomationFramework project and all dependent projects
    // If needed, other projects can use it's own objects by binding them from Instance 
    public static RuntimeConfigurationModel RuntimeConfigurationModel { get; private set; }
    public static LoggingModel LoggingModel { get; private set; }
    public static TestRunConfig TestRunConfig { get; private set; }
    public static ConditionalWaitConfigurationModel DefaultConditionalWaitConfiguration { get; private set; }

    public void AddStaticSources(ConfigurationManager configurationManagerInstance)
    {
        configurationManagerInstance.AddJsonFile(AppSettingsFileName);
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, ConditionalWaitSettingsFileName));
        configurationManagerInstance.AddEnvironmentVariables();
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance)
    {
        // No ThreadStatic sources to add
    }

    /// <summary>
    /// Use with static configuration Properties. Call in [BeforeTestRun] hook.
    /// </summary>
    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance)
    {
        RuntimeConfigurationModel = configurationManagerInstance.GetRequiredSection(RuntimeConfigurationModel.JsonSectionName).Get<RuntimeConfigurationModel>();
        LoggingModel = configurationManagerInstance.GetSection(LoggingModel.JsonSectionName).Get<LoggingModel>();
        TestRunConfig = new TestRunConfig
        {
            TestRunStartTime = DateTime.Now
        };
        DefaultConditionalWaitConfiguration = configurationManagerInstance.GetRequiredSection($"{ConditionalWaitConfigurationModel.JsonSectionName}:Default").Get<ConditionalWaitConfigurationModel>();
    }

    /// <summary>
    /// Use with ThreadStatic configuration Properties (Test Thread scoped). Call in [BeforeScenario] hook
    /// </summary>
    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance)
    {
        // No ThreadStatic objects to configure
    }
}
