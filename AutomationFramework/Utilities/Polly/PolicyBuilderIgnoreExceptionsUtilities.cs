using NLog;
using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PolicyBuilderUtilities
{
    public static PolicyBuilder<T> HandleExceptionsFromIgnoreList<T>(IList<Type>? exceptionsToIgnore)
    {
        IsExceptionListValid(exceptionsToIgnore, strict: true);

        return Policy<T>
            .Handle<Exception>(exception => exceptionsToIgnore!.Any(type => type.IsInstanceOfType(exception)));
    }

    public static PolicyBuilder<T> OrExceptionsFromIgnoreList<T>(this PolicyBuilder<T> handleResultPolicyBuilder,
        IList<Type>? exceptionsToIgnore)
    {
        if (IsExceptionListValid(exceptionsToIgnore))
        {
            handleResultPolicyBuilder
                .Or<Exception>(exception => exceptionsToIgnore!.Any(type => type.IsInstanceOfType(exception)));
        }

        return handleResultPolicyBuilder;
    }

    private static bool IsExceptionListValid(IList<Type>? exceptionsToIgnore, bool strict = false)
    {
        if (exceptionsToIgnore is null || exceptionsToIgnore.Count == 0)
        {
            if (strict)
            {
                throw new ArgumentNullException(nameof(exceptionsToIgnore), "exceptionsToIgnore list cannot be null or empty");
            }

            LogManager.GetCurrentClassLogger().Warn("Method to ignore exception list is called but the exception list is null or empty.");
            return false; // Exception list is null or empty
        }

        if (exceptionsToIgnore.Any(exceptionType => !typeof(Exception).IsAssignableFrom(exceptionType)))
        {
            throw new ArgumentException("All types to be ignored must derive from System.Exception", nameof(exceptionsToIgnore));
        }

        return true; // All checks passed
    }
}
