﻿using Polly.Retry;

namespace AutomationFramework.Models.Configuration;

public class ConditionalWaitConfigurationModel
{
    public const string JsonSectionName = "ConditionalWait";

    public ConditionalWaitConfigurationModel(TimeSpan Timeout, TimeSpan BackOffDelay, double? Factor = null, RetryBackoffType? BackoffType = null)
    {
        this.Timeout = Timeout;
        this.BackOffDelay = BackOffDelay;
        if (Factor is not null) this.Factor = (double) Factor;
        if (BackoffType is not null) this.BackoffType = (RetryBackoffType) BackoffType;
    }

    public TimeSpan Timeout { get; set; }
    public TimeSpan BackOffDelay { get; set; }
    public double Factor { get; set; } = 1.0;
    public RetryBackoffType BackoffType { get; set; } = RetryBackoffType.Constant;

    // Adding +1 to be sure that overall retry time will exceed timeout
    // Works for constant, linear and exponential timeouts with positive factors (> 1.0)
    // I find this approach is most suitable, flexible and resource optimized for ConditionalWait implementation
    // We can add some code to handle negative factors (< 1.0), but it's not necessary
    public int RetryCount => (int) Timeout.Divide(BackOffDelay) + 1;
}
