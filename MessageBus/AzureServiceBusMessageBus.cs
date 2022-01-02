using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {

        private string connectionAzureBus =
            "Endpoint=sb://vastagrestaurant.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SfmKH5gP66D65881hBUQBtYDRi42AUsxUYICIQUxcVo=";

        public async Task PublishMessage(BaseMessage message, string topic)
        {
            await using var client = new ServiceBusClient(connectionAzureBus);

            ServiceBusSender sender = client.CreateSender("checkoutmessagetopic");

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage)) {
                CorrelationId = Guid.NewGuid().ToString()
            };
            await sender.SendMessageAsync(finalMessage);
            await sender.DisposeAsync();
        }
    }
}
