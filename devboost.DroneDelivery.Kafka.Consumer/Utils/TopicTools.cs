using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace devboost.DroneDelivery.Kafka.Consumer.Utils
{
    public class TopicTools
    {

        public static void CreateTopicAsync(string bootstrapServers, string topicName)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();
            try
            {
                 adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = topicName, ReplicationFactor = 1, NumPartitions = 1 } }).Wait();
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }
    }
}