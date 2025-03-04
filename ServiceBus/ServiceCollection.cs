using Azure.Messaging.ServiceBus;
using ServiceBus.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBus.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServiceBusServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusConnection = configuration["ServiceBusSettings:Connection"];

            services.AddSingleton(sp => new ServiceBusClient(serviceBusConnection));

            // Register the factory as a singleton
            services.AddSingleton<ServiceBusFactory>();

            services.AddTransient<IPublisher, EmailPublisher>();
            services.AddTransient<IPublisher, SMSPublisher>();
            services.AddTransient<NotificationManager>();
            return services;
        }
    }
}
