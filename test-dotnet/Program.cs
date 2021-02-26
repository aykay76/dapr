using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;

namespace test_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var client = new DaprClientBuilder().Build();
            int i = 1;

            while (true)
            {
                var eventData = new { Beat = i++, Time = DateTime.Now, };

                try
                {
                    Console.WriteLine("boop");
                    await client.PublishEventAsync("pubsub", "heartbeat", eventData);
                }
                catch (DaprException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                await Task.Delay(1000);
            }
        }
    }
}
