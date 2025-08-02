using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Queries.UserQueries.ObterUserByIdQuery;

public class ObterUserByIdQueryInput : CommandInput<ObterUserByIdQueryItem>
{
    public Guid IdUser { get; set; }

}