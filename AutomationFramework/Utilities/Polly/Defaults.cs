using Polly;

namespace AutomationFramework.Utilities.Polly;

public class Defaults
{
    public static Policy<T> GetDefaultWaitPolicy<T>(TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null,
            IList<Type> exceptionsToIgnore = null)
        // public Policy GetWaitPolicy<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null, IList<Exception> exceptionsToIgnore = null)
    {
        var policy = Policy.HandleResult<T>(x => x != null);

        // if (exceptionsToIgnore != null)
        // {
        //     foreach (var exceptionToIgnore in exceptionsToIgnore)
        //     {
        //         policy.Or<InvalidOperationException>();
        //     }
        // }

        var x = Policy.HandleResult<Func<T>>(x => x.Invoke() != null).Retry(5);

        Func<bool> xxx = new Func<bool>(() => true);

        // x.Execute<>(() =>
        // {
        //     return xxx;
        // });

        return policy.WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(2));
    }
    //
    // public void Get()
    // {
    //     var x = GetWaitPolicy<InvalidOperationException>(() => true);
    // }
}
