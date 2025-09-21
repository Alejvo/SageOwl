using Application.Qualifications.GetByTeamId;
using Application.Qualifications.GetByUserId;
using Application.Qualifications.Save;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class QualificationsController : ControllerBase
{
    private readonly ISender _sender;

    public QualificationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("teamId/{teamId}")]
    public async Task<IActionResult> GetQualificationByTeamId([FromRoute] Guid teamId)
    {
        var res = await _sender.Send(new GetQualificationsByTeamIdQuery(teamId));

        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpGet("userId/{userId}")]
    public async Task<IActionResult> GetQualificationByUserId([FromRoute] Guid userId)
    {
        var res = await _sender.Send(new GetQualificationsByUserIdQuery(userId));

        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> SaveQualifications([FromBody] SaveQualificationCommand command)
    {
        var res = await _sender.Send(command);

        return res.IsSuccess ? Created() : BadRequest();
    }
}
