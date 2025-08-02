//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;
//using CentralTask.Core.DTO.Worker;

//namespace CentralTask.Broker;

//public abstract class EventConsumerBase : BackgroundService
//{
//    protected readonly int _countThreads;
//    protected readonly ILogger<EventConsumerBase> _logger;
//    protected ConnectionFactory _factory;
//    protected IConnection _connection;
//    protected IModel _channel;
//    protected readonly string _queueName;
//    protected readonly MessageBrokerConfig _messageBrokerConfig;

//    public EventConsumerBase(string queueName, ILogger<EventConsumerBase> logger,
//        IOptions<MessageBrokerConfig> messageBrokerConfig,
//        IOptions<ParallelProcessingConfig> parallelProcessingConfig)
//    {
//        _queueName = queueName;
//        _countThreads = parallelProcessingConfig.Value.ThreadsCount;
//        _messageBrokerConfig = messageBrokerConfig.Value;
//        _logger = logger;
//    }

//    public override Task StartAsync(CancellationToken cancellationToken)
//    {
//        _logger.LogInformation($"Worker is starting [{_queueName}]");

//        _factory = new ConnectionFactory()
//        {
//            HostName = _messageBrokerConfig.Host,
//            Port = _messageBrokerConfig.Port,
//            UserName = _messageBrokerConfig.User,
//            Password = _messageBrokerConfig.Password,
//            ConsumerDispatchConcurrency = _countThreads
//        };

//        if (!string.IsNullOrEmpty(_messageBrokerConfig.Virtualhost))
//        {
//            _factory.VirtualHost = _messageBrokerConfig.Virtualhost;
//        }

//        _factory.AutomaticRecoveryEnabled = true;

//        _connection = _factory.CreateConnection();
//        _connection.ConnectionShutdown += ConnectionShutdown;
//        _channel = _connection.CreateModel();

//        _channel.QueueDeclare(queue: _queueName,
//                         durable: true,
//                         exclusive: false,
//                         autoDelete: false,
//                         arguments: null);

//        var properties = _channel.CreateBasicProperties();
//        properties.Persistent = true;

//        _logger.LogInformation($"Worker is started [{_queueName}]");
//        _logger.LogInformation($"Message consumer active [{_queueName}]: {_queueName}");

//        return base.StartAsync(cancellationToken);
//    }

//    protected void PrepareAndExecuteProcess(object sender, BasicDeliverEventArgs eventArgs)
//    {
//        var message = "Payload da mensagem não processado";
//        try
//        {
//            message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
//            var messageQueue = JsonSerializer.Deserialize<MessageEventModel>(message);
//            _logger.LogInformation($"Message received: {messageQueue.Message}/{messageQueue.Reprocess}");

//            var success = ExecuteProcess(messageQueue);
//            _channel.BasicAck(eventArgs.DeliveryTag, false);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, $"Ocorreu um erro ao processar mensagem da fila: {_queueName}: {message}");
//        }
//    }

//    protected virtual bool ExecuteProcess(MessageEventModel messageEvent)
//    {
//        _logger.LogWarning($"[Queue: {_queueName}] Nenhuma ação registrada");
//        return false;
//    }

//    private void ConnectionShutdown(object sender, ShutdownEventArgs e)
//    {
//        _logger.LogError($"Connection change [{_queueName}]: {e.ReplyText}");
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        try
//        {
//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += PrepareAndExecuteProcess;

//            _channel.BasicConsume(queue: _queueName,
//                                    autoAck: false,
//                                    consumer: consumer);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError($"Ocorreu um erro ao criar canal para consumir fila [{_queueName}]. {ex}");
//        }

//        return Task.CompletedTask;
//    }

//    public override async Task StopAsync(CancellationToken cancellationToken)
//    {
//        _logger.LogWarning($"Worker is stopping [{_queueName}]");
//        await base.StopAsync(cancellationToken);
//        _logger.LogWarning($"Worker is stopped [{_queueName}]");
//    }

//}
