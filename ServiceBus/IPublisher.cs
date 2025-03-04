using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public interface IPublisher
    {
        Task PublishAsync(string queueMessage, CancellationToken cancellationToken);
    }
}
