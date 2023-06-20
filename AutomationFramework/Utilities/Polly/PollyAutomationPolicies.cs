using AutomationFramework.Configuration;
using NLog;
using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Func<T, bool> IsNullDelegate<T>() => t => t == null;
    public static Func<bool, bool> IsFalseDelegate => t => t != true;

    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate)
    {
        var waitAndRetryPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .WaitAndRetry(
            AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.RetryCount + 1,    // TODO delete +1
            retryAttempt => AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.PollingInterval,
            (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry attempt #{arg3 + 1}");
            });

        var timeoutPolicy = Policy.Timeout(AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.Timeout);

        return timeoutPolicy.Wrap(waitAndRetryPolicy);
    }
}
