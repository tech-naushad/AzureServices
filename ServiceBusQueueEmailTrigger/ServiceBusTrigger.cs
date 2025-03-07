using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace ServiceBusQueueEmailTrigger
{
    public class ServiceBusTrigger
    {
        private readonly ILogger<ServiceBusTrigger> _logger;
     

        public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
        {
            _logger = logger;      
        }

        [Function(nameof(ServiceBusTrigger))]
        public async Task Run(
            [ServiceBusTrigger("email-queue", 
            Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            [DurableClient] DurableTaskClient client,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var body = Encoding.UTF8.GetString(message.Body);
            await client.ScheduleNewOrchestrationInstanceAsync(new TaskName(nameof(Orchestrator)), body);

            // Complete the message
            await messageActions.CompleteMessageAsync(message); 
        }
    }
}
