using Application.Users.Commands.Create;
using Application.Users.Commands.Update;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController : ApiController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize
        )
    {
        var res = await _sender.Send(new GetUsersQuery(page,pageSize,searchTerm, sortColumn,sortOrder));
        return res.IsSuccess ? Ok(res.Value.Items) : Problem(res.Errors);
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var res = await _sender.Send(new GetUserByIdQuery(id));
        return  res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : Problem(res.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : Problem(res.Errors);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}
