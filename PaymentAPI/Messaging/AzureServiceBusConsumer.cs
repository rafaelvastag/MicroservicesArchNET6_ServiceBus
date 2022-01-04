using Azure.Messaging.ServiceBus;
using MessageBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentAPI.Messages;
using PaymentProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionPayment;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderPaymentUpdateResultTopic;

        private ServiceBusProcessor orderPaymentProcessor;
        private IMessageBus _messageBus;

        private readonly IProcessorPayment _paymentProcessor;
        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(IProcessorPayment paymentProcessor, IConfiguration configuration, IMessageBus messageBus)
        {
            _paymentProcessor = paymentProcessor;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionPayment = _configuration.GetValue<string>("SubscriptionPayment");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderPaymentUpdateResultTopic = _configuration.GetValue<string>("OrderPaymentUpdateResultTopic");
            

            var client = new ServiceBusClient(serviceBusConnectionString);
            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);
            _messageBus = messageBus;
        }

        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += OnPaymentMessageReceived;
            orderPaymentProcessor.ProcessErrorAsync += ErrorHander;

            await orderPaymentProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {

            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();
        }

        Task ErrorHander(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnPaymentMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = _paymentProcessor.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId
            };

            try
            {
                await _messageBus.PublishMessage(updatePaymentResultMessage, orderPaymentUpdateResultTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
