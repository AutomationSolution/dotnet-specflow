using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate, ConditionalWaitConfigurationModel waitConfiguration)
    {
        var waitAndRetryPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .WaitAndRetry(
                CalculateBackoff(waitConfiguration), 
                (_, _, arg3, _) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry attempt #{arg3 + 1}");
            });

        var timeoutPolicy = Policy.Timeout(waitConfiguration.Timeout);

        return timeoutPolicy.Wrap(waitAndRetryPolicy);
    }

    private static IEnumerable<TimeSpan>? CalculateBackoff(ConditionalWaitConfigurationModel configuration)
    {
        LogManager.GetCurrentClassLogger().Debug($"Executing {nameof(CalculateBackoff)} method. {configuration.Dump()}");
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
