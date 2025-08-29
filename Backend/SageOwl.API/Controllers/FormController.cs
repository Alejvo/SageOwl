using Application.Forms.Create;
using Application.Forms.GetByTeamId;
using Application.Forms.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FormController : ControllerBase
{
    private readonly ISender _sender;

    public FormController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateForm(CreateFormCommand command)
    {
        var res = await _sender.Send(command);

        return res.IsSuccess ? Created() : BadRequest(res);
    }

    [HttpGet("teamId/{teamId:guid}")]
    public async Task<IActionResult> GetFormByTeamId([FromRoute] Guid teamId)
    {
        var res = await _sender.Send(new GetFormByTeamIdQuery(teamId));

        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpGet("userId/{userId:guid}")]
    public async Task<IActionResult> GetFormByUserId([FromRoute] Guid userId)
    {
        var res = await _sender.Send(new GetPendingFormsByUserIdQuery(userId));

        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }
}
