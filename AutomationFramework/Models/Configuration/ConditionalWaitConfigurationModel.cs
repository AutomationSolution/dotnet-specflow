﻿using Polly.Retry;

namespace AutomationFramework.Models.Configuration;

public class ConditionalWaitConfigurationModel
{
    public const string JsonSectionName = "ConditionalWait";

    public TimeSpan Timeout { get; set; }
    public TimeSpan BackOffDelay { get; set; }
    public double Factor { get; set; }
    public RetryBackoffType BackoffType { get; set; }

    // Adding +1 to be sure that overall retry time will exceed timeout
    // Works for constant, linear and exponential timeouts with positive factors (> 1.0)
    // I find this approach is most suitable, flexible and resource optimized for ConditionalWait implementation
    // We can add some code to handle negative factors (< 1.0), but it's not necessary
    public int RetryCount => (int) Timeout.Divide(BackOffDelay) + 1;
}
