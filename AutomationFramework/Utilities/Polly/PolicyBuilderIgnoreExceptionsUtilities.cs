using Polly;

namespace AutomationFramework.Utilities.Polly;

public static class PolicyBuilderUtilities
{
    public static PolicyBuilder<T> HandleExceptionsFromIgnoreList<T>(IList<Type>? exceptionsToIgnore)
    {
        PolicyBuilder<T> result;
        
        if (exceptionsToIgnore is not null && exceptionsToIgnore.Count > 0)
        {
            var ignoreExceptionsList = IgnoreExceptionTypes(exceptionsToIgnore);
            result = Policy<T>
                .Handle<Exception>(exception => ignoreExceptionsList.Any(type => type.IsInstanceOfType(exception)));
        }
        else
        {
            throw new ArgumentNullException(nameof (exceptionsToIgnore), "exceptionTypes cannot be null or empty");
        }

        return result;
    }

    public static PolicyBuilder<T> OrExceptionsFromIgnoreList<T>(this PolicyBuilder<T> handleResultPolicyBuilder, IList<Type>? exceptionsToIgnore)
    {
        if (exceptionsToIgnore is not null && exceptionsToIgnore.Count > 0)
        {
            var ignoreExceptionsList = IgnoreExceptionTypes(exceptionsToIgnore);
            handleResultPolicyBuilder
                .Or<Exception>(exception => ignoreExceptionsList.Any(type => type.IsInstanceOfType(exception)));
        }

        return handleResultPolicyBuilder;
    }
    
    private static List<Type> IgnoreExceptionTypes(IList<Type> exceptionsToIgnore)
    {
        var resultedExceptionList = new List<Type>();
        
        if (exceptionsToIgnore == null)
            throw new ArgumentNullException(nameof (exceptionsToIgnore), "exceptionTypes cannot be null");

        foreach (var exceptionType in exceptionsToIgnore)
        {
            if (!typeof (Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException("All types to be ignored must derive from System.Exception", nameof (exceptionsToIgnore));
        }
        resultedExceptionList.AddRange(exceptionsToIgnore);

        return resultedExceptionList;
    }
}
