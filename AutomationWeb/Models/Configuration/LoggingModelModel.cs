namespace AutomationWeb.Models.Configuration;

public class LoggingModel
{
    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }
    public string System { get; set; }
    public string Microsoft { get; set; }
}