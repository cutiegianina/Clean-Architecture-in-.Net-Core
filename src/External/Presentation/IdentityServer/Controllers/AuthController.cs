using Application.Dtos;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IdentityServer.Models;

namespace IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;

    public AuthController(IMediator mediator) =>
        _mediator = mediator;

    [HttpPost("login")]
    public async Task<ActionResult<UserSignInDto>> SignIn([FromBody] UserCredentialRequest request)
    {
        var response = await _mediator.Send(new CheckUserSignInStatusQuery(request.Username, request.Password));

        return Ok(response);
    }
}