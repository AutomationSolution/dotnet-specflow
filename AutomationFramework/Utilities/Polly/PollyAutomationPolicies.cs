using AutomationFramework.Configuration;
using NLog;
using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PollyAutomationPolicies
{
    public static Policy<T> GetWaitForNotNullPolicy<T>()
    {
        return Policy<T>.HandleResult(t => t == null).WaitAndRetry(
            AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.RetryCount,
            retryAttempt => AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.PollingInterval,
            (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Result: {delegateResult.Result}. Execution attempt #{arg3 + 1}");
            });
    }

    public static Policy<bool> GetWaitForTruePolicy()
    {
        return Policy<bool>.HandleResult(t => t != true).WaitAndRetry(
            AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.RetryCount,
            retryAttempt => AutomationFrameworkConfiguration.DefaultConditionalWaitConfiguration.PollingInterval,
            (delegateResult, span, arg3, arg4) =>
            {
                LogManager.GetCurrentClassLogger()
                    .Debug($"Didn't get expected result after code execution. Code execution attempt #{arg3 + 1}");
            });
    }
}
