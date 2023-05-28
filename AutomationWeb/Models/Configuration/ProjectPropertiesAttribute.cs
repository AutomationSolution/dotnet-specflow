namespace AutomationWeb.Models.Configuration;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class ProjectPropertiesAttribute : Attribute
{
    public string Environment { get; }
    public string UserSecretsId { get; }
    
    public ProjectPropertiesAttribute(string environment, string userSecretsId)
    {
        Environment = environment;
        UserSecretsId = userSecretsId;
    }
}