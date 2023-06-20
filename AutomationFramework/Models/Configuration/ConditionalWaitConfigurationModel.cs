namespace AutomationFramework.Models.Configuration;

public class ConditionalWaitConfigurationModel
{
    public const string JsonSectionName = "ConditionalWait";

    public TimeSpan Timeout { get; set; }
    public TimeSpan PollingInterval { get; set; }
    public int RetryCount => (int) Timeout.Divide(PollingInterval);
    public bool IsOneMorePollingNeeded => SecondsLeftForLastPolling.Milliseconds > 0;
    public TimeSpan SecondsLeftForLastPolling => Timeout.Subtract(PollingInterval.Multiply(RetryCount));
}
