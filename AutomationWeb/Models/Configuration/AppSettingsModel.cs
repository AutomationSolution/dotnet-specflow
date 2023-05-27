namespace AutomationWeb.Models.Configuration;

public class AppSettingsModel
{
    public Logging Logging { get; set; }
    public string DOTNETCORE_ENVIRONMENT { get; set; }
}

public class Logging
{
    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }
    public string System { get; set; }
    public string Microsoft { get; set; }
}