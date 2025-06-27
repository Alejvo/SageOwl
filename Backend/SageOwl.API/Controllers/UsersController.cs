using Application.Users.Create;
using Application.Users.GetAll;
using Application.Users.GetById;
using Application.Users.Update;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
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
        var res = await _sender.Send(new GetUsersQuery(searchTerm,sortColumn,sortOrder,page,pageSize));
        return res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var res = await _sender.Send(new GetUserByIdQuery(id));
        return  res.IsSuccess ? Ok(res.Value) : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? Created() : BadRequest(ModelState);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var res = await _sender.Send(command);
        return res.IsSuccess ? NoContent() : BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}
