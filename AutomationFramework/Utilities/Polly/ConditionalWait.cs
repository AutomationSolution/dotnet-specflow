using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitFor<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null, ConditionalWaitConfigurationModel? configuration = null)
    {
        configuration ??= AutomationFrameworkConfiguration.ConstantConditionalWait;
        
        var conditionDelegate = PollyPredicates.IsNullPredicate<T>();
        var waitForNotNullPolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        return WaitForWrapper(waitForNotNullPolicy, condition, conditionDelegate, timeout, pollingInterval, message, exceptionsToIgnore, codePurpose);
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null, ConditionalWaitConfigurationModel? configuration = null)
    {
        configuration ??= AutomationFrameworkConfiguration.ConstantConditionalWait;
        
        var conditionDelegate = PollyPredicates.IsFalsePredicate;
        var waitForTruePolicy = PollyAutomationPolicies.ConditionalWaitPolicy(conditionDelegate, configuration);

        WaitForWrapper(waitForTruePolicy, condition, conditionDelegate, timeout, pollingInterval, message, exceptionsToIgnore, codePurpose);
    }

    private static T WaitForWrapper<T>(Policy<T> policy, Func<T> codeToExecute, Func<T, bool> conditionDelegate, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string? codePurpose = null)
    {
        // TODO add exceptionsToIgnore handling
        // TODO implement passing explicit timeouts
        // TODO implement passing explicit polling invtervals

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
            throw new TimeoutException($"Result is null. {message}");
        }

        return executionResult;
    }
}
