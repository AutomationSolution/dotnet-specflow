﻿using AutomationFramework.Enums.Configuration;

namespace AutomationFramework.Models.Configuration;

public class RuntimeConfigurationModel
{
    public string AutomationEnvironment { get; set; }
    public SecretsClient SecretsClient { get; set; }
}