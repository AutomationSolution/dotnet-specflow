using System;
using System.Linq;
using System.Reflection;
using AutomationMobile.Configuration;
using AutomationMobile.Enums.FrameworkAdditions;
using MethodDecorator.Fody.Interfaces;

namespace AutomationMobile.Utilities.FrameworkAdditions;

/// <summary>
///     Attribute that identifies needed context and switches to it.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ScreenContextAttribute : Attribute, IMethodDecorator
{
    public ScreenContextAttribute(MobileScreenContext screenType, params ApplicationName[] applicationNameList)
    {
        ScreenContext = screenType;
        ContextCount = 0;
        ApplicationNameList = applicationNameList;
    }

    /// <summary>
    ///     Name of platform that screen relates to.
    /// </summary>
    private MobileScreenContext ScreenContext { get; }

    private ApplicationName[] ApplicationNameList { get; }

    /// <summary>
    ///     Count of needed context from list of contexts.
    /// </summary>
    private int ContextCount { get; }

    public void Init(object instance, MethodBase method, object[] args)
    {
        // Not needed to perform actions on init
    }

    public void OnEntry()
    {
        if (ApplicationNameList is null || !ApplicationNameList.Any())
        {
            ContextUtils.ChangeContext(ScreenContext, ContextCount);
        }
        else
        {
            if (ApplicationNameList.Contains(AutomationMobileConfiguration.DeviceConfigModel.ApplicationName))
            {
                ContextUtils.ChangeContext(ScreenContext, ContextCount);
            }
            else
            {
                var defaultContext = Enum.GetValues<MobileScreenContext>().First(enumValue => enumValue != ScreenContext);
                ContextUtils.ChangeContext(defaultContext, ContextCount);
            }
        }
    }

    public void OnExit()
    {
        // Not needed to perform actions on method exit
    }

    public void OnException(Exception exception)
    {
        // Not needed to perform actions on exception caught
    } 
}