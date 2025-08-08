using Application.Teams.Create;
using Application.Teams.GetById;
using Application.Teams.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly ISender _sender;

    public TeamController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var res = await _sender.Send(new GetTeamByIdQuery(id));
        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : BadRequest();
    }
}
