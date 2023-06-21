using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate, Func<T> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null)
    {
        var waitAndRetryPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .WaitAndRetry(
                CalculateBackoff(waitConfiguration), 
                (_, _, arg3, _) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry #{arg3} (Execution #{arg3 + 1}):");
            });

        var timeoutPolicy = Policy
            .Timeout(waitConfiguration.Timeout);

        var timeoutRejectedFallbackPolicy = Policy<T>
            .Handle<TimeoutRejectedException>()
            .Fallback(codeToExecute.Invoke);

        var timeoutExceededAndUnexpectedResultException = new TimeoutException(
            $"Unexpected code execution result on final retry attempt after {waitConfiguration.Timeout} timeout. Reason: {failReason ?? "reason not specified"}");
        var unexpectedResultFallbackPolicy = Policy<T>
            .HandleResult(handleResultDelegate)
            .Fallback(() => throw timeoutExceededAndUnexpectedResultException);

        return unexpectedResultFallbackPolicy.Wrap(timeoutRejectedFallbackPolicy.Wrap(timeoutPolicy.Wrap(waitAndRetryPolicy)));
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
