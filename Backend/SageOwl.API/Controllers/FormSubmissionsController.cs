using Application.FormSubmissions.Commands.Create;
using Application.FormSubmissions.Queries.GetByFormId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
public class FormSubmissionsController : ApiController
{
    private readonly ISender _sender;

    public FormSubmissionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("formId/{formId:guid}")]
    public async Task<IActionResult> GetFormSubmissionsById([FromRoute] Guid formId)
    {
        var res = await _sender.Send(new GetFormSubmissionsByFormIdQuery(formId));

        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> GetFormSubmissionsById([FromBody] CreateFormSubmissionCommand command)
    {
        var res = await _sender.Send(command);

        return res.IsSuccess ? Created() : Problem(res.Errors);
    }
}
