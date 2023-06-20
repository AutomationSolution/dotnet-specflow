using NLog;
using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitFor<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var waitForNotNullPolicy = PollyAutomationPolicies.GetWaitForNotNullPolicy<T>();

        return WaitForWrapper(waitForNotNullPolicy, condition, timeout, pollingInterval, message, exceptionsToIgnore, codePurpose);
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var waitForTruePolicy = PollyAutomationPolicies.GetWaitForTruePolicy();

        WaitForWrapper(waitForTruePolicy, condition, timeout, pollingInterval, message, exceptionsToIgnore, codePurpose);
    }

    private static T WaitForWrapper<T>(Policy<T> policy, Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
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
        var result = policy.Execute(condition);

        // Assert policy
        if (result == null)
        {
            throw new TimeoutException($"Result is null. {message}");
        }

        return result;
    }
}
