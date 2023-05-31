using Microsoft.Extensions.Configuration;

namespace AutomationFramework.Configuration;

public interface IAutomationConfiguration
{
    public void AddStaticSources(ConfigurationManager configurationManagerInstance);
    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance);
    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance);
    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance);
}