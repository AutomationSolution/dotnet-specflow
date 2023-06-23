namespace AutomationFramework.Models.Configuration;

public class LoggingModel
{
    public const string JsonSectionName = "Logging";

    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }
    public string System { get; set; }
    public string Microsoft { get; set; }
}
