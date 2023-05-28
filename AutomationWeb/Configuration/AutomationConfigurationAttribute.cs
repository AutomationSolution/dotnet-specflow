namespace AutomationWeb.Configuration;

[AttributeUsage(AttributeTargets.Assembly)]
sealed class AutomationConfigurationAttribute : Attribute
{
    public string AutomationEnvironment { get; }
    public string UserSecretsId { get; }
    
    public AutomationConfigurationAttribute(string automationEnvironment, string userSecretsId)
    {
        AutomationEnvironment = automationEnvironment;
        UserSecretsId = userSecretsId;
    }
}