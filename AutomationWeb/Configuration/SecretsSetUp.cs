﻿using System.Reflection;
using AutomationFramework.Configuration;
using AutomationFramework.Enums.Configuration;
using AutomationWeb.Models.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class SecretsSetUp
{
    public static void Setup(SecretsClient secretsClient, ConfigurationManager configurationManager)
    {
        switch (secretsClient)
        {
            case SecretsClient.Local:
                LocalSecretsSetup(configurationManager);
                break;
            case SecretsClient.Jenkins:
                JenkinsSecretsSetup(configurationManager);
                break;
            case SecretsClient.Azure:
                AzureSecretsSetup(configurationManager);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(secretsClient), secretsClient, $"There are no secrets handling implementations found for {nameof(secretsClient)}");
        }
    }

    private static void LocalSecretsSetup(ConfigurationManager configurationManager)
    {
        configurationManager.AddUserSecrets(Assembly.GetCallingAssembly(), optional: false);
    }

    private static void JenkinsSecretsSetup(ConfigurationManager configurationManager)
    {
        // In Jenkins we should use credentials feature and pass defined credentials in environment variables.
        // So we're just making sure that they are added
        configurationManager.AddEnvironmentVariables();
    }

    private static void AzureSecretsSetup(ConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("azureKeyVaultSettings.json", optional: false);

        var azureSettings = configurationManager.GetRequiredSection("AzureKeyVault").Get<AzureSettingsModel>();

        var clientSecretCredential = new ClientSecretCredential(azureSettings.TenantId.ToString(), azureSettings.ClientId.ToString(),
            azureSettings.ClientSecret.ToString());

        var secretClient = new SecretClient(
            new Uri(string.Format(azureSettings.KeyVaultEndPoint, azureSettings.KeyVaultName)),
            clientSecretCredential);

        configurationManager.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
    }
}