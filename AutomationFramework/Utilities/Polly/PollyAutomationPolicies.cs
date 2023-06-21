using AutomationFramework.Models.Configuration;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> ConditionalWaitPolicy<T>(Func<T, bool> handleResultDelegate, Func<T> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null, IList<Type>? exceptionsToIgnore = null)
    {
        var negatedHandleResultDelegateForPolly = NegateFuncTBoolResult(handleResultDelegate);

        var handleResultPolicyBuilder = Policy<T>
            .HandleResult(negatedHandleResultDelegateForPolly)
            .OrExceptionsFromIgnoreList(exceptionsToIgnore);

        var waitAndRetryPolicy = handleResultPolicyBuilder
            .WaitAndRetry(
                CalculateBackoff(waitConfiguration), 
                (_, _, arg3, _) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Unexpected code execution result. Retry #{arg3} (Execution #{arg3 + 1}):");
            });

        var timeoutPolicy = Policy
            .Timeout(waitConfiguration.Timeout, TimeoutStrategy.Optimistic);

        var timeoutRejectedFallbackPolicy = Policy<T>
            .Handle<TimeoutRejectedException>()
            .Fallback(codeToExecute.Invoke);

        var timeoutExceededAndUnexpectedResultException = new TimeoutException(
            $"Unexpected code execution result on final retry attempt after {waitConfiguration.Timeout} timeout. Reason: {failReason ?? "reason not specified"}");
        var unexpectedResultFallbackPolicy = handleResultPolicyBuilder
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

    private static Func<T, bool> NegateFuncTBoolResult<T>(Func<T, bool> conditionPredicate)
    {
        return t => !conditionPredicate(t); 
    }
}
