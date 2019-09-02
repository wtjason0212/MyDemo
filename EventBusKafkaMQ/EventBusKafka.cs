using Confluent.Kafka;
using EvenBus.Abstractions;
using EvenBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusKafkaMQ
{
    public class EventBusKafka : IEventBus, IDisposable
    {
        const string TOPIC_NAME = "my-testtopic";
        private readonly ILogger<EventBusKafka> _logger;

        public EventBusKafka(ILogger<EventBusKafka> logger)
        {
            _logger = logger;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async void Publish(IntegrationEvent @event)
        {
            //判斷連線
            //todo

            //publish
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync(TOPIC_NAME, new Message<Null, string> { Value = @event.Id });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }


            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
