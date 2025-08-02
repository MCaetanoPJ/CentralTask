using CentralTask.Core.DTO.Worker;

namespace CentralTask.Broker.Interfaces;

public interface IEventPublisher
{
    Task SendAsync(MessageRequestModel queueRequest);
}