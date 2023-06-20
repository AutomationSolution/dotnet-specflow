using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;
using NLog;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitForAndGetResult<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForAndGetResult(condition, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForAndGetResult<T>(Func<T> condition, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsNullPredicate<T>();

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
        var conditionPredicate = PollyPredicates.IsFalsePredicate;

        WaitForWrapper(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }

    private static T WaitForWrapper<T>(Func<T> codeToExecute, Func<T, bool> conditionPredicate, ConditionalWaitConfigurationModel configuration, string? reason = null,
        IList<Type> exceptionsToIgnore = null, string? codePurpose = null)
    {
        var policy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionPredicate, configuration);
        
        // TODO add exceptionsToIgnore handling

        // Set up logging message
        var messageBeforeExecution = $"Trying to execute code in {nameof(WaitForTrue)} method. ";
        if (codePurpose != null)
        {
            messageBeforeExecution += $"Code purpose: {codePurpose}. ";
        }

        messageBeforeExecution += "Execution attempt #1";
        
        // Execute policy
        LogManager.GetCurrentClassLogger().Debug(messageBeforeExecution);
        T? executionResult; // TODO replace with callback policy if possible
        try
        {
            executionResult = policy.Execute(codeToExecute);
        }
        catch (TimeoutRejectedException)
        {
            executionResult = codeToExecute.Invoke();
        }

        // Assert policy
        if (conditionPredicate.Invoke(executionResult))
        {
            throw new TimeoutException($"Unexpected code execution result on final retry attempt after {configuration.Timeout} timeout. Reason: {reason ?? "reason not specified"}");
        }

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
