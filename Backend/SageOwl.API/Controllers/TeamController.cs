using Application.Teams.Commands.Create;
using Application.Teams.Commands.Delete;
using Application.Teams.Commands.Update;
using Application.Teams.Queries.GetById;
using Application.Teams.Queries.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController : ApiController
{
    private readonly ISender _sender;

    public TeamController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("userId/{userId:guid}")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var res = await _sender.Send(new GetTeamsByUserIdQuery(userId));
        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var res = await _sender.Send(new GetTeamByIdQuery(id));
        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : Problem(res.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : Problem(res.Errors);
    }

    [HttpDelete]
    [Route("{teamId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid teamId)
    {
        var res = await _sender.Send(new DeleteTeamCommand(teamId));
        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }
}
