using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class ServiceBusHelper
{
    private readonly string _connectionString;
    private readonly string _topicName = "news";

    public ServiceBusHelper(IConfiguration configuration)
    {
        _connectionString = configuration["AzureServiceBus:ConnectionString"];
    }

    public async Task SendMessageAsync(string messageContent, string messageType)
    {
        await using var client = new ServiceBusClient(_connectionString);
        ServiceBusSender sender = client.CreateSender(_topicName);

        // Create a message with a custom property
        ServiceBusMessage message = new ServiceBusMessage(messageContent);
        message.ApplicationProperties["msg"] = messageType; // Adding the custom property "msg"

        await sender.SendMessageAsync(message);
    }
}
