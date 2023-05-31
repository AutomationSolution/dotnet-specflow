using Microsoft.Extensions.Configuration;

namespace AutomationFramework.Configuration;

public static class AutomationConfigurationManager
{
    private static ConfigurationManager? _configurationManagerInstance;
    
    public static ConfigurationManager Instance => _configurationManagerInstance ??= new ConfigurationManager();
}