using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace CentralTask.Broker
{
    public class EventConnection : IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;

        public EventConnection(string hostName)
        {
            _factory = new ConnectionFactory { HostName = hostName };
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = await _factory.CreateConnectionAsync();
            }

            return await _connection.CreateChannelAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}