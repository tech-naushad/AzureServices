using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class NotificationManager
    {
        private readonly IEnumerable<IPublisher> _publishers;

        public NotificationManager(IEnumerable<IPublisher> publishers)
        {
            _publishers = publishers;
        }

        public async Task NotifyAsync(string queueMessage, CancellationToken cancellationToken)
        {
            foreach (var publisher in _publishers)
            {
                await publisher.PublishAsync(queueMessage, cancellationToken);
            }
           // var tasks = _publishers.Select(service => service.PublishAsync(queueMessage, cancellationToken));
           // await Task.WhenAll(tasks);
        }
    }
}
