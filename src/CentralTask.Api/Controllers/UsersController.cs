using CentralTask.Application.Commands.UserCommands.AlterarUserCommand;
using CentralTask.Application.Commands.UserCommands.CriarUserCommand;
using CentralTask.Application.Commands.UserCommands.DeleteUserCommand;
using CentralTask.Application.Commands.UserCommands.InativarAtivarUserCommand;
using CentralTask.Application.Commands.UserCommands.RealizarLoginCommand;
using CentralTask.Application.Queries.UserQueries.ObterTodosUserQuery;
using CentralTask.Application.Queries.UserQueries.ObterUserByIdQuery;
using CentralTask.Application.Queries.UserQueries.ObterUsersQueryPaginated;
using CentralTask.Core.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CentralTask.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> CriarUser([FromBody] CriarUserCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RealizarLoginCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpPut()]
    //[Authorize]
    public async Task<IActionResult> AlterarUser([FromBody] AlterarUserCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpDelete()]
    //[Authorize]
    public async Task<IActionResult> DeletarUser([FromBody] DeleteUserCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpPut("status")]
    //[Authorize]
    public async Task<IActionResult> AtualizarStatusUser([FromBody] InativarUserCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpGet("by-id")]
    //[Authorize]
    public async Task<IActionResult> GetByIdUser([FromQuery] ObterUserByIdQueryInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpGet()]
    //[Authorize]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserQueryInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpGet("paginado")]
    //[Authorize]
    public async Task<IActionResult> GetPaginadoUser([FromQuery] ObterUsersQueryPaginatedInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }
}