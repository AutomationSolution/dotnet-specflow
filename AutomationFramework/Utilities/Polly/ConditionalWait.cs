using AutomationFramework.Configuration;
using AutomationFramework.Models.Configuration;
using NLog;
using Polly.Timeout;

namespace AutomationFramework.Utilities.Polly;

public static class ConditionalWait
{
    public static T WaitForPredicateAndGetResult<T>(Func<T> condition, Func<T,bool> conditionPredicate, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForPredicateAndGetResult(condition, conditionPredicate, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForPredicateAndGetResult<T>(Func<T> codeToExecute, Func<T,bool> conditionPredicate, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        return PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, failReason, exceptionsToIgnore, codePurpose)
            .Execute(codeToExecute);
    }
    
    public static T WaitForAndGetResult<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        return WaitForAndGetResult(condition, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static T WaitForAndGetResult<T>(Func<T> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsNotNullPredicate<T>();

        return PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, failReason, exceptionsToIgnore, codePurpose)
            .Execute(codeToExecute);
    }

    public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? backoffDelay = null, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var configuration = InitConditionalWaitConfigurationModel(timeout, backoffDelay);

        WaitForTrue(condition, configuration, failReason, exceptionsToIgnore, codePurpose);
    }
    
    public static void WaitForTrue(Func<bool> codeToExecute, ConditionalWaitConfigurationModel waitConfiguration, string? failReason = null,
        IList<Type> exceptionsToIgnore = null, string codePurpose = null)
    {
        var conditionPredicate = PollyPredicates.IsTruePredicate;

        PollyAutomationPolicies
            .ConditionalWaitPolicy(conditionPredicate, codeToExecute, waitConfiguration, failReason, exceptionsToIgnore, codePurpose)
            .Execute(codeToExecute);
    }

    private static ConditionalWaitConfigurationModel InitConditionalWaitConfigurationModel(TimeSpan? timeout = null, TimeSpan? backoffDelay = null)
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
