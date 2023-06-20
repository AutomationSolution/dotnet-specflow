using AutomationWeb.Enums.FrameworkAdditions;

namespace AutomationWeb.Models.Configuration;

public class WebEnvironment
{
    public const string JsonSectionName = "WebEnvironment";

    public WebExecutionPlatform WebExecutionPlatform { get; set; }
}
