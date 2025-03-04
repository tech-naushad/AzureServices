using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Factory
{
    public class ServiceBusFactory
    {
        private readonly ServiceBusClient _client;

        public ServiceBusFactory(ServiceBusClient client)
        {
            _client = client;
        }

        public ServiceBusSender CreateSender(string queueOrTopicName)
        {
            return _client.CreateSender(queueOrTopicName);
        }
    }
}
