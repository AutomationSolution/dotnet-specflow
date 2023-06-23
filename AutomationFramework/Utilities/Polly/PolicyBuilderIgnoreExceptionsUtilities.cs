using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PolicyBuilderUtilities
{
    public static PolicyBuilder<T> HandleExceptionsFromIgnoreList<T>(IList<Type>? exceptionsToIgnore)
    {
        PolicyBuilder<T> result;

        CheckExceptionsList(exceptionsToIgnore);

        result = Policy<T>
            .Handle<Exception>(exception => exceptionsToIgnore!.Any(type => type.IsInstanceOfType(exception)));

        return result;
    }

    public static PolicyBuilder<T> OrExceptionsFromIgnoreList<T>(this PolicyBuilder<T> handleResultPolicyBuilder,
        IList<Type>? exceptionsToIgnore)
    {
        CheckExceptionsList(exceptionsToIgnore);

        handleResultPolicyBuilder
            .Or<Exception>(exception => exceptionsToIgnore!.Any(type => type.IsInstanceOfType(exception)));

        return handleResultPolicyBuilder;
    }

    private static void CheckExceptionsList(IList<Type>? exceptionsToIgnore)
    {
        if (exceptionsToIgnore is null || exceptionsToIgnore.Count == 0)
            throw new ArgumentNullException(nameof(exceptionsToIgnore), "exceptionsToIgnore list cannot be null or empty");

        foreach (var exceptionType in exceptionsToIgnore)
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException("All types to be ignored must derive from System.Exception", nameof(exceptionsToIgnore));
        }
    }
}
