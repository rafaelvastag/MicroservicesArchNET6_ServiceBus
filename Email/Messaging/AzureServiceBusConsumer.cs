using Azure.Messaging.ServiceBus;
using MessageBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Email.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Email.Models.Messages;

namespace Email.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly EmailRepository _emailRepository;
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionEmail;
        private readonly string orderPaymentUpdateResultTopic;

        private ServiceBusProcessor emailProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(EmailRepository emailRepository, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionEmail = _configuration.GetValue<string>("SubscriptionEmail");
            orderPaymentUpdateResultTopic = _configuration.GetValue<string>("OrderPaymentUpdateResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);
            emailProcessor = client.CreateProcessor(orderPaymentUpdateResultTopic, subscriptionEmail);
        }

        public async Task Start()
        {
            emailProcessor.ProcessMessageAsync += OnEmailMessageReceived;
            emailProcessor.ProcessErrorAsync += ErrorHander;

            await emailProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await emailProcessor.StopProcessingAsync();
            await emailProcessor.DisposeAsync();

        }

        Task ErrorHander(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnEmailMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage result = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            try
            {
                await _emailRepository.SendAndLogEmail(result);
                await args.CompleteMessageAsync(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
