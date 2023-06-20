using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate, ConditionalWaitConfigurationModel configuration)
    {
        var waitAndRetryPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .WaitAndRetry(
                CalculateBackoff(configuration), 
                (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry attempt #{arg3 + 1}");
            });

        var timeoutPolicy = Policy.Timeout(configuration.Timeout);

        return timeoutPolicy.Wrap(waitAndRetryPolicy);
    }

    private static IEnumerable<TimeSpan>? CalculateBackoff(ConditionalWaitConfigurationModel configuration)
    {
        LogManager.GetCurrentClassLogger().Debug($"Executing {nameof(CalculateBackoff)} method. BackoffDelay: {configuration.BackOffDelay}; RetryCount: {configuration.RetryCount}; BackoffType: {configuration.BackoffType}; Factor: {configuration.Factor}");
        switch (configuration.BackoffType)
        {
            case RetryBackoffType.Constant:
                return Backoff.ConstantBackoff(configuration.BackOffDelay, configuration.RetryCount);
            case RetryBackoffType.Linear:
                return Backoff.LinearBackoff(configuration.BackOffDelay, configuration.RetryCount, configuration.Factor);
            case RetryBackoffType.Exponential:
                return Backoff.ExponentialBackoff(configuration.BackOffDelay, configuration.RetryCount, configuration.Factor);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
