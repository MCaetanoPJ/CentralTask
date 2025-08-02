using MediatR;
using Microsoft.AspNetCore.Mvc;
using CentralTask.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using CentralTask.Application.Commands.UsersCommands;
using CentralTask.Application.Queries.UsersQueries;

namespace CentralTask.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    //[Authorize]
    public async Task<IActionResult> CriarUsers([FromBody] CriarUsersCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpPut("")]
    //[Authorize]
    public async Task<IActionResult> AlterarUsers([FromBody] AlterarUsersCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpDelete("")]
    //[Authorize]
    public async Task<IActionResult> DeletarUsers([FromBody] DeletarUsersCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpPut("status")]
    //[Authorize]
    public async Task<IActionResult> AtualizarStatusUsers([FromBody] AtualizarStatusUsersCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpGet("by-id")]
    //[Authorize]
    public async Task<IActionResult> GetByIdUsers([FromQuery] GetByIdUsersInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

    [HttpGet("paginado")]
    //[Authorize]
    public async Task<IActionResult> GetPaginadoUsers([FromQuery] GetPaginadoUsersInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }

}
