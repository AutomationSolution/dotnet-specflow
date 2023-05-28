using System.Reflection;
using AutomationFramework.Configuration;
using AutomationFramework.Enums.Configuration;
using AutomationWeb.Extensions.Configuration;
using AutomationWeb.Models.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class SecretsSetUp
{
    public static void Setup(SecretsClient secretsClient)
    {
        switch (secretsClient)
        {
            case SecretsClient.Local:
                LocalSecretsSetup();
                break;
            case SecretsClient.Jenkins:
                JenkinsSecretsSetup();
                break;
            case SecretsClient.Azure:
                AzureSecretsSetup();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(secretsClient), secretsClient, $"There are no secrets handling implementations found for {nameof(secretsClient)}");
        }
    }

    private static void LocalSecretsSetup()
    {
        AutomationConfiguration.Instance.AddUserSecrets(Assembly.GetCallingAssembly(), optional: false);
    }

    private static void JenkinsSecretsSetup()
    {
        // In Jenkins we should use credentials feature and pass defined credentials in environment variables.
        // So we're just making sure that they are added
        AutomationConfiguration.Instance.AddEnvironmentVariables();
    }

    private static void AzureSecretsSetup()
    {
        // const string secretName = "mySecret";
        // var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
        // var kvUri = $"https://{keyVaultName}.vault.azure.net";
        // var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        // var secret = client.GetSecretAsync(secretName).Result;
        AutomationConfiguration.Instance.AddJsonFileFromResourcesDirectory("azureSettings.json", optional: false);

        var azureSettings = AutomationConfiguration.Instance.GetRequiredSection("Azure").Get<AzureSettingsModel>();

        var secretClient = new SecretClient(
            new Uri($"https://{azureSettings.KeyVaultName}.vault.azure.net/"),
            new DefaultAzureCredential());

        AutomationConfiguration.Instance.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
    }
}