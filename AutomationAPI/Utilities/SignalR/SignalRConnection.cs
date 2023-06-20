using AutomationAPI.Configuration;
using AutomationFramework.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using NLog;

namespace AutomationAPI.Utilities.SignalR;

public sealed class SignalRConnection : IDisposable
{
    private bool isConnected;
    private readonly List<Tuple<string, string>> receivedMessages = new();
    private readonly string sendMessageMethod = "SendMessage";
    private readonly string receiveMessageMethod = "ReceiveMessage";
    private HubConnection? hubConnection;

    public async Task Connect()
    {
        var url = AutomationAPIConfiguration.SignalRData.SignalREndpoint.Append(AutomationAPIConfiguration.SignalRData.SignalRHubPath);
        hubConnection = new HubConnectionBuilder()
            .WithUrl(url, options =>
            {
                options.Url = url; // options usage reference
            })
            .ConfigureLogging(logging => { logging.AddConsole(); })
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>(receiveMessageMethod, (name, message) =>
        {
            receivedMessages.Add(new Tuple<string, string>(name, message));
            LogManager.GetCurrentClassLogger().Debug($"SignalR Message Received: {name}; {message}");
        });

        hubConnection.StartAsync().ContinueWith(t => { isConnected = !t.IsFaulted; }).Wait();

        LogManager.GetCurrentClassLogger().Warn($"Connection Id: {hubConnection.ConnectionId}");
    }

    public async Task SendMessage(string name, string message)
    {
        if (hubConnection is null)
            throw new NullReferenceException("Connection is null. Unable to send a message before establishing a connection");
        await hubConnection.InvokeCoreAsync(sendMessageMethod, args: new object?[] {name, message});
    }

    public bool IsConnectionEstablished()
    {
        return isConnected;
    }

    public List<Tuple<string, string>> GetReceivedMessages()
    {
        return receivedMessages;
    }

    public void Dispose()
    {
        hubConnection?.DisposeAsync().AsTask().Wait();
    }
}
