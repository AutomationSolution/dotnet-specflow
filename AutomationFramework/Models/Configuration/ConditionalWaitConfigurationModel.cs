namespace AutomationFramework.Models.Configuration;

public class ConditionalWaitConfigurationModel
{
    public TimeSpan Timeout { get; set; }
    public TimeSpan PollingInterval { get; set; }
    public int RetryCount => (int) Timeout.Divide(PollingInterval);
}
