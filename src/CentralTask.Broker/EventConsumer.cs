using CentralTask.Core.DTO.Worker;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CentralTask.Broker
{
    public class EventConsumer
    {
        private readonly EventConnection _connection;
        private readonly ILogger<EventConsumer> _logger;

        public EventConsumer(EventConnection connection, ILogger<EventConsumer> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task ConsumerAsync(string queueName)
        {
            var channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var messageObj = JsonSerializer.Deserialize<MessageEventModel>(message);

                    if (messageObj != null)
                    {
                        _logger.LogInformation($"Mensagem recebida da fila {queueName}: {messageObj.Message}");
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    else
                    {
                        _logger.LogWarning("Mensagem recebida não pôde ser desserializada.");
                        await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                    await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
            };


            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer
            );

            _logger.LogInformation($"Consumidor iniciado para a fila: {queueName}");
        }
    }
}
