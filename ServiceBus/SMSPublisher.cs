using Azure.Messaging.ServiceBus;
using ServiceBus.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class SMSPublisher : IPublisher
    {
        private readonly string _queueName;
        private readonly ServiceBusFactory _serviceBusFactory;

        public SMSPublisher(ServiceBusFactory serviceBusFactory, IConfiguration configuration)
        {
            _queueName = configuration["ServiceBusSettings:SMSQueue"];
            _serviceBusFactory = serviceBusFactory;
        }
        public async Task PublishAsync(string queueMessage, CancellationToken cancellationToken)
        {
            var sender = _serviceBusFactory.CreateSender(_queueName);
            await sender.SendMessageAsync(new ServiceBusMessage(queueMessage), cancellationToken);
        }
    }
}
