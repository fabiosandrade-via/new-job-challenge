using Confluent.Kafka;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace new_job_challenge.carrefour.infra.producer.kafka
{
    public static class ProducerBrokerKafka
    {
        public static void Send<T>(T entity) where T: class
        {
            string topic = "testtopic";

            var config = new ClientConfig()
            {
                BootstrapServers = "127.0.0.1:9092",

            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var key = "movimento-conta";
                var val = JObject.FromObject(new { entity }).ToString(Formatting.None);

                Console.WriteLine($"Produzindo mensagem: {key} {val}");

                producer.Produce(topic, new Message<string, string> { Key = key, Value = val },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Falha para enviar mensagem: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Mensagem produzida para: {deliveryReport.TopicPartitionOffset}");
                        }
                    });

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}