using Confluent.Kafka;
using new_job_challenge.carrefour.domain.Entities;
using Newtonsoft.Json;

namespace new_job_challenge.carrefour.infra.consumer.kafka
{
    public static class ConsumerBrokerKafka
    {
        public static void Read()
        {
            var config = new ClientConfig()
            {
                BootstrapServers = "127.0.0.1:9092",

            };

            var consumerConfig = new ConsumerConfig(config);
            consumerConfig.GroupId = "group-keyvault2";
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            consumerConfig.EnableAutoCommit = false;
            string topic = "testtopic";

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };
            
            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
            {
                consumer.Subscribe(topic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        AmountOperationAccountEntity amountOperationAccountEntity = (AmountOperationAccountEntity)JsonConvert.DeserializeObject(cr.Message.Value);
                        
                        AccountMovimentConsumer.IAccountMovementRedisRepository.Save(amountOperationAccountEntity, AccountMovimentConsumer.IDistributedCache);
                        AccountMovimentConsumer.IAccountMovementPostgresRepository.AmountOperationAccountEntities.Add(amountOperationAccountEntity);

                        Console.WriteLine($"Consumo de registro com a chave {cr.Message.Key} e valor {cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}