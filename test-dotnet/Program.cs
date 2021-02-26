using System;
using System.Threading.Tasks;
using Dapr.Client;

namespace test_dotnet
{
    class Program
    {
        static async Task MainAsync(string[] args)
        {
            var client = new DaprClientBuilder().Build();
            int i = 1;

            while (true)
            {
                var eventData = new { Beat = i++, Time = DateTime.Now, };
                await client.PublishEventAsync("pubsub", "heartbeat", eventData);

                await Task.Delay(1000);
            }
        }
    }
}
