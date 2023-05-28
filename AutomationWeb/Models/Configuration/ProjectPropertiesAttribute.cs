namespace AutomationWeb.Models.Configuration;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class ProjectPropertiesAttribute : Attribute
{
    public string RuntimePropertyExample { get; }
    public string UserSecretsId { get; }
    
    public ProjectPropertiesAttribute(string runtimePropertyExample, string userSecretsId)
    {
        RuntimePropertyExample = runtimePropertyExample;
        UserSecretsId = userSecretsId;
    }
}