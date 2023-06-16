using NLog;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitFor<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        // TODO add exceptionsToIgnore handling

        // Configure policy
        var waitForNotNullPolicy = PollyAutomationPolicies.GetWaitForNotNullPolicy<T>();

        // Execute policy
        var messageBeforeExecution = $"Trying to execute code in {nameof(WaitForTrue)} method. ";
        if (codePurpose != null)
        {
            messageBeforeExecution += $"Code purpose: {codePurpose}. ";
        }

        messageBeforeExecution += "Execution attempt #1";
        
        LogManager.GetCurrentClassLogger().Debug(messageBeforeExecution);
        var result = waitForNotNullPolicy.Execute(condition);

        // Assert policy
        if (result == null)
        {
            throw new TimeoutException($"Result is null. {message}");
        }

        return result;
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
        IList<Type> exceptionsToIgnore = null)
    {
        // TODO add exceptionsToIgnore handling

        // Arrange policy
        var waitForTruePolicy = PollyAutomationPolicies.GetWaitForTruePolicy();

        // Act policy
        LogManager.GetCurrentClassLogger().Debug($"Trying to execute code in {nameof(WaitForTrue)} method. Code execution attempt #1");
        var result = waitForTruePolicy.Execute(condition);

        // Assert policy
        if (result != true)
        {
            throw new TimeoutException($"Result is not true but {result}. {message}");
        }
    }
}
