using AutomationMobile.Enums.FrameworkAdditions;

namespace AutomationMobile.Utilities.Aquality;

/// <summary>
///     Attribute that identifies platform of screen.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class CustomApplicationNameAttribute : Attribute
{
    public CustomApplicationNameAttribute(params ApplicationName[] applicationName)
    {
        ApplicationNameList = applicationName;
    }

    /// <summary>
    ///     Name of platform that screen relates to.
    /// </summary>
    public ApplicationName[] ApplicationNameList { get; }
}
