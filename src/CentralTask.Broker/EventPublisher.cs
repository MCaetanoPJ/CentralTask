//using CentralTask.Broker.Interfaces;
//using CentralTask.Core.DTO.Worker;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;

//namespace CentralTask.Broker;

//public class EventPublisher : IEventPublisher
//{
//    private readonly ILogger<EventPublisher> _logger;
//    private readonly ConnectionFactory _factory;

//    public EventPublisher(ILogger<EventPublisher> logger,
//        IOptions<MessageBrokerConfig> messageBrokerConfig)
//    {
//        _logger = logger;

//        _factory = new ConnectionFactory()
//        {
//            HostName = messageBrokerConfig.Value.Host,
//            Port = messageBrokerConfig.Value.Port,
//            UserName = messageBrokerConfig.Value.User,
//            Password = messageBrokerConfig.Value.Password
//        };

//        if (!string.IsNullOrEmpty(messageBrokerConfig.Value.Virtualhost))
//        {
//            _factory.VirtualHost = messageBrokerConfig.Value.Virtualhost;
//        }
//    }

//    public async Task SendAsync(MessageRequestModel queueRequest)
//    {
//        using (var connection = _factory.CreateConnectionAsync())
//        {
//            using (var channel = connection.CreateModel())
//            {
//                await SendAsync(channel, queueRequest);
//            }
//        }
//    }

//    private async Task SendAsync(IModel channel, MessageRequestModel queueRequest)
//    {
//        channel.QueueDeclare(queue: queueRequest.QueueEvent.ToString(),
//                                durable: true,
//                                exclusive: false,
//                                autoDelete: false,
//                                arguments: null);

//        var messageQueue = new MessageEventModel()
//        {
//            Id = Guid.NewGuid(),
//            CreatedAt = DateTime.Now,
//            Message = queueRequest.Message,
//            MessageType = queueRequest.MessageType,
//            Reprocess = queueRequest.Reprocess
//        };

//        var messageData = JsonSerializer.Serialize(messageQueue);

//        var body = Encoding.UTF8.GetBytes(messageData);

//        channel.BasicPublish(exchange: "",
//                                routingKey: queueRequest.QueueEvent.ToString(),
//                                basicProperties: null,
//                                body: body);

//        _logger.LogInformation($"Message send: {queueRequest.QueueEvent.ToString()}/{queueRequest.Message}");
//    }

//}
