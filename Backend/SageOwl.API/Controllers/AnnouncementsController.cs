using Application.Announcements.Create;
using Application.Announcements.GetAll;
using Application.Announcements.GetByTeamId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AnnouncementsController : ControllerBase
{
    private readonly ISender _sender;

    public AnnouncementsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("teamId/{teamId:guid}")]
    public async Task<IActionResult> GetAnnoncementByTeamId([FromRoute] Guid teamId)
    {
        var res = await _sender.Send(new GetAnnouncementByTeamIdQuery(teamId));
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAnnouncements()
    {
        var res = await _sender.Send(new GetAnnouncementsQuery());
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : BadRequest(res.Errors);
    }
}
