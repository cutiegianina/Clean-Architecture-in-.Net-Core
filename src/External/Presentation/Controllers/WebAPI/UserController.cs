using Application.Dtos;
using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.WebAPI;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IMediator _mediator;

    public UserController(IMediator mediator) =>
		_mediator = mediator;

	[HttpPost("register")]
	public async Task<ActionResult<int>> RegisterUser([FromBody] UserDto request)
	{
		if (request is null)
			return BadRequest();

		var response = await _mediator.Send(new CreateUserCommand(request));
		return Ok(response);
	}
}
