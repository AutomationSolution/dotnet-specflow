using AutomationFramework.Configuration;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate)
    {
        var retryCount = AutomationFrameworkConfiguration.ConstantConditionalWait.RetryCount + 1;    // + 1 to make sure that 
        
        var constantBackoff = Backoff.ConstantBackoff(AutomationFrameworkConfiguration.ConstantConditionalWait.BackOffDelay, retryCount);
        var linearBackoff = Backoff.LinearBackoff(AutomationFrameworkConfiguration.LinearConditionalWait.BackOffDelay, retryCount);
        var exponentialBackoff = Backoff.ExponentialBackoff(AutomationFrameworkConfiguration.ExponentialConditionalWait.BackOffDelay, retryCount);

        var waitAndRetryPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .WaitAndRetry(
                exponentialBackoff, 
                (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry attempt #{arg3 + 1}");
            });

        var timeoutPolicy = Policy.Timeout(AutomationFrameworkConfiguration.ConstantConditionalWait.Timeout);

        return timeoutPolicy.Wrap(waitAndRetryPolicy);
    }
}
