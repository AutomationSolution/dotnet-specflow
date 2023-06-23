using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitForPredicateAndGetResult<T>(Func<T> codeToExecute, Func<T, bool> conditionPredicate, TimeSpan? timeout = null,
        TimeSpan? backoffDelay = null, IList<Type>? exceptionsToIgnore = null, string? failReason = null, string? codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForPredicateAndGetResult(codeToExecute, conditionPredicate, configuration, exceptionsToIgnore, failReason, codePurpose);
    }

    public static T WaitForPredicateAndGetResult<T>(Func<T> codeToExecute, Func<T, bool> conditionPredicate,
        ConditionalWaitConfigurationModel waitConfiguration, IList<Type>? exceptionsToIgnore = null, string? failReason = null,
        string? codePurpose = null)
    {
        return PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, exceptionsToIgnore, failReason, codePurpose)
            .Execute(codeToExecute);
    }

    public static T WaitForAndGetResult<T>(Func<T> codeToExecute, TimeSpan? timeout = null, TimeSpan? backoffDelay = null,
        IList<Type>? exceptionsToIgnore = null, string? failReason = null, string? codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForAndGetResult(codeToExecute, configuration, exceptionsToIgnore, failReason, codePurpose);
    }

    public static T WaitForAndGetResult<T>(Func<T> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration,
        IList<Type>? exceptionsToIgnore = null, string? failReason = null, string? codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsNotNullPredicate<T>();

        return PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, exceptionsToIgnore, failReason, codePurpose)
            .Execute(codeToExecute);
    }

    public static void WaitForTrue(Func<bool> codeToExecute, TimeSpan? timeout = null, TimeSpan? backoffDelay = null,
        IList<Type>? exceptionsToIgnore = null, string? failReason = null, string? codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        WaitForTrue(codeToExecute, configuration, exceptionsToIgnore, failReason, codePurpose);
    }

    public static void WaitForTrue(Func<bool> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration,
        IList<Type>? exceptionsToIgnore = null, string? failReason = null, string? codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsTruePredicate;

        PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, exceptionsToIgnore, failReason, codePurpose)
            .Execute(codeToExecute);
    }

    private static ConditionalWaitConfigurationModel InitConditionalWaitConfigurationModel(TimeSpan? timeout = null,
        TimeSpan? backoffDelay = null)
    {
        var configuration = AutomationFrameworkConfiguration.ConstantConditionalWait;
        if (timeout is not null)
        {
            configuration.Timeout = (TimeSpan) timeout;
        }

        if (backoffDelay is not null)
        {
            configuration.BackOffDelay = (TimeSpan) backoffDelay;
        }

        return configuration;
    }
}
