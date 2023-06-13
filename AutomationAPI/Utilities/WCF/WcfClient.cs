using AutomationAPI.Service;

namespace AutomationAPI.Utilities.WCF;

public sealed class WcfClient : IDisposable
{
    private ServiceClient? client;

    public ServiceClient ClientInstance => client ??= new ServiceClient();

    public void Dispose()
    {
        ClientInstance.Close();
    }
}
