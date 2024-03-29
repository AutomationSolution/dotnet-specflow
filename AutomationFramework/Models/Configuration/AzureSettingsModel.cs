﻿namespace AutomationWeb.Models.Configuration;

public class AzureSettingsModel
{
    public const string JsonSectionName = "AzureKeyVault";

    public string KeyVaultName { get; set; }
    public string KeyVaultEndPoint { get; set; }
    public Guid TenantId { get; set; }
    public Guid ClientId { get; set; }
    public Guid ClientSecret { get; set; }
}
