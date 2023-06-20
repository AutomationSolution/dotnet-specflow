using AutomationMobile.Enums.FrameworkAdditions;

namespace AutomationMobile.Models.Configuration;

public class MobileEnvironment
{
    public const string JsonSectionName = "MobileEnvironment";

    public MobileExecutionPlatform MobileExecutionPlatform { get; set; }
}
