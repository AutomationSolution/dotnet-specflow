using AutomationFramework.Configuration;
using NLog;
using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> GetWaitForNotNullPolicy<T>()
    {
        return ConditionalWaitPolicy(IsNullDelegate<T>());
    }

    public static Policy<bool> GetWaitForTruePolicy()
    {
        return ConditionalWaitPolicy(IsFalseDelegate);
    }

    private static Func<T, bool> IsNullDelegate<T>() => t => t == null;
    private static Func<bool, bool> IsFalseDelegate => t => t != true;

    private static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate)
    {
        return Policy<T>.HandleResult(handleResultDelegate).WaitAndRetry(
            AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.RetryCount,
            retryAttempt => AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.PollingInterval,
            (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Didn't get expected result after code execution. Code execution attempt #{arg3 + 1}");
            });
    }
}
