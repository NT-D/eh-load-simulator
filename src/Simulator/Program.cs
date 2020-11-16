using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace Simulator
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            // TODO: Need validation
            // TODO: Investigate why VS Code debug mode doesn't read env variable
            string _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            string _eventHubName = Environment.GetEnvironmentVariable("EventHubName");
            int _numberOfMessages = Int32.Parse(Environment.GetEnvironmentVariable("NumberOfMessages"));
            int _intervalMiliSeconds = Int32.Parse(Environment.GetEnvironmentVariable("IntervalMiliSeconds"));

            await using (var ehClient = new EventHubProducerClient(_connectionString, _eventHubName))
            {
                TimerCallback timerDelegate = new TimerCallback(async (Object o) => await SendMessagesToEventHub(ehClient, _numberOfMessages));
                Timer timer = new Timer(timerDelegate, null, 0, _intervalMiliSeconds);
                Console.WriteLine("Press enter to stop...");
                Console.ReadLine();
                // Stop timer
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
            }
        }

        private static async Task SendMessagesToEventHub(EventHubProducerClient ehClient, int numberOfMessages)
        {
            using EventDataBatch eventBatch = await ehClient.CreateBatchAsync();
            for (var i = 0; i < numberOfMessages; i++)
            {
                var sensorId = i % 4;
                // TODO: Add random value
                var sensorData = new Thermometer($"sensor-{sensorId}", 10);

                // TODO: Can separate option 
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sensorData, options))));
            }

            try
            {
                string currentTime = DateTime.UtcNow.ToLocalTime().ToString("T");
                Console.WriteLine($"{currentTime} : {numberOfMessages} messages sent.");
                await ehClient.SendAsync(eventBatch);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
            }
        }
    }
}
