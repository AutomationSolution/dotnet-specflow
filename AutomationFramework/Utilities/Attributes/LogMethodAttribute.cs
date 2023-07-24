using System.Reflection;
using MethodDecorator.Fody.Interfaces;
using NLog;

namespace AutomationFramework.Utilities.Attributes;

[Obsolete("The attribute is not working while accessing from other assemblies. " +
          "Possibly, it should be reworked to work correctly in the same assembly.")]
[AttributeUsage(AttributeTargets.Method)]
public sealed class LogMethodAttribute : Attribute, IMethodDecorator
{
    private readonly string additionalStringOnEntry = string.Empty;
    private readonly string additionalStringOnExit = string.Empty;
    private readonly LogLevel logLevel;
    private MethodBase? methodBase;

    public LogMethodAttribute()
    {
        logLevel = LogLevel.Debug;
    }
    
    public LogMethodAttribute(LogLevel logLevel)
    {
        this.logLevel = logLevel;
    }
    
    public LogMethodAttribute(string additionalString, LogLevel logLevel)
    {
        additionalStringOnEntry = additionalString;
        additionalStringOnExit = additionalString;
        this.logLevel = logLevel;
    }

    public LogMethodAttribute(string additionalStringOnEntry, string additionalStringOnExit, LogLevel logLevel)
    {
        this.additionalStringOnEntry = additionalStringOnEntry;
        this.additionalStringOnExit = additionalStringOnExit;
        this.logLevel = logLevel;
    }

    public void Init(object instance, MethodBase? method, object[] args)
    {
        methodBase = method;
    }

    public void OnEntry()
    {
        var entryLogMessage = $"Started method \"{methodBase?.Name}\" {additionalStringOnEntry}";

        LogManager.GetCurrentClassLogger().Log(logLevel, entryLogMessage);
    }

    public void OnExit()
    {
        var exitLogMessage = $"Finished method \"{methodBase?.Name}\" {additionalStringOnExit}";

        LogManager.GetCurrentClassLogger().Log(logLevel, exitLogMessage);
    }

    public void OnException(Exception exception) 
    { 
        // Not needed to perform actions on exception caught
    }
}
