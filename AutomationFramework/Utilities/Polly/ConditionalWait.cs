using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;
using NLog;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitForPredicateAndGetResult<T>(Func<T> condition, Func<T,bool> conditionPredicate, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForPredicateAndGetResult(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForPredicateAndGetResult<T>(Func<T> condition, Func<T,bool> conditionPredicate, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        return WaitForWrapper(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForAndGetResult<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForAndGetResult(condition, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForAndGetResult<T>(Func<T> condition, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsNotNullPredicate<T>();

        return WaitForWrapper(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        WaitForTrue(condition, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static void WaitForTrue(Func<bool> condition, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsTruePredicate;

        WaitForWrapper(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }

    private static T WaitForWrapper<T>(Func<T> codeToExecute, Func<T, bool> conditionPredicate, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string? codePurpose = null)
    {
        var policy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration);

        // TODO add exceptionsToIgnore handling

        // TODO consider deleting
        // Set up logging message
        var messageBeforeExecution = $"Trying to execute code in {nameof(WaitForTrue)} method. ";
        if (codePurpose != null)
        {
            messageBeforeExecution += $"Code purpose: {codePurpose}. ";
        }

        messageBeforeExecution += "Execution attempt #1";
        LogManager.GetCurrentClassLogger().Debug(messageBeforeExecution);
        
        // Execute policy
        var executionResult = policy.Execute(codeToExecute);

        // TODO consider deleting
        LogManager.GetCurrentClassLogger().Debug($"Code execution result in {nameof(WaitForTrue)} method have met expected predicate. Result: {executionResult}");

        return executionResult;
    }

    private static ConditionalWaitConfigurationModel InitConditionalWaitConfigurationModel(TimeSpan? timeout = null, TimeSpan? backoffDelay = null)
    {
        var configuration = AutomationFrameworkConfiguration.ConstantConditionalWait;
        if (timeout is not null)
        {
            configuration.Timeout = (TimeSpan) timeout;
        }

        if (backoffDelay is not null)
        {
            configuration.BackOffDelay = (TimeSpan) backoffDelay;
        }

        return configuration;
    }
}
