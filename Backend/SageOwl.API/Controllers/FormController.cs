using Application.Forms.Commands.Create;
using Application.Forms.Commands.Delete;
using Application.Forms.Commands.Update;
using Application.Forms.Queries.GetById;
using Application.Forms.Queries.GetByTeamId;
using Application.Forms.Queries.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class FormController : ApiController
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

        return res.IsSuccess ? Created() : Problem(res.Errors);
    }

    [HttpGet("teamId/{teamId:guid}")]
    public async Task<IActionResult> GetFormByTeamId([FromRoute] Guid teamId)
    {
        var res = await _sender.Send(new GetFormByTeamIdQuery(teamId));

        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpGet("userId/{userId:guid}")]
    public async Task<IActionResult> GetFormByUserId([FromRoute] Guid userId)
    {
        var res = await _sender.Send(new GetPendingFormsByUserIdQuery(userId));

        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpGet("{formId:guid}")]
    public async Task<IActionResult> GetFormById([FromRoute] Guid formId)
    {
        var res = await _sender.Send(new GetFormByIdQuery(formId));

        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateForm([FromBody] UpdateFormCommand command)
    {
        var res = await _sender.Send(command);

        return res.IsSuccess ? NoContent(): Problem(res.Errors);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteForm([FromBody] DeleteFormCommand command)
    {
        var res = await _sender.Send(command);

        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }
}
