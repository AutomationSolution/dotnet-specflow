using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitFor<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
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
        
        var conditionDelegate = PollyPredicates.IsNullPredicate<T>();
        var waitForNotNullPolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        return WaitForWrapper(waitForNotNullPolicy, condition, conditionDelegate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitFor<T>(Func<T> condition, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionDelegate = PollyPredicates.IsNullPredicate<T>();
        var waitForNotNullPolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        return WaitForWrapper(waitForNotNullPolicy, condition, conditionDelegate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
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
        
        var conditionDelegate = PollyPredicates.IsFalsePredicate;
        var waitForTruePolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        WaitForWrapper(waitForTruePolicy, condition, conditionDelegate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static void WaitForTrue(Func<bool> condition, ConditionalWaitConfigurationModel configuration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionDelegate = PollyPredicates.IsFalsePredicate;
        var waitForTruePolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        WaitForWrapper(waitForTruePolicy, condition, conditionDelegate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }

    private static T WaitForWrapper<T>(Policy<T> policy, Func<T> codeToExecute, Func<T, bool> conditionDelegate, ConditionalWaitConfigurationModel configuration, string? reason = null,
        IList<Type> exceptionsToIgnore = null, string? codePurpose = null)
    {
        // TODO add exceptionsToIgnore handling
        // TODO implement passing explicit timeouts
        // TODO implement passing explicit polling intervals

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
        if (conditionDelegate.Invoke(executionResult))
        {
            throw new TimeoutException($"Unexpected code execution result on final retry attempt after {configuration.Timeout} timeout. Reason: {reason ?? "reason not specified"}");
        }

        return executionResult;
    }
}
