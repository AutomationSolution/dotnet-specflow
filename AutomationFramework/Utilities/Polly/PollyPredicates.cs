namespace AutomationFramework.Utilities.Polly;

public static class PollyPredicates
{
    public static Func<T, bool> IsNullPredicate<T>() => t => t == null;
    public static readonly Func<bool, bool> IsFalsePredicate = t => t != true;
}
