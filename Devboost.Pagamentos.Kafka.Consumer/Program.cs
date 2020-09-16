using System;
using Microsoft.Extensions.DependencyInjection;

namespace Devboost.Pagamentos.Kafka.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);
            services.BuildServiceProvider().GetService<ConsoleApp>().Run();
        }
    }
}
