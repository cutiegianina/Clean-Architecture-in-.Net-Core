using Application.Common.Enums;
using Application.Common.Interfaces.Services;
using Application.Dtos;
using Application.Models.IdentityServer;
using Application.Users.Queries;
using IdentityServer.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.IdentityServer;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IMediator mediator, IJwtTokenService jwtTokenService) =>
        (_mediator, _jwtTokenService) = (mediator, jwtTokenService);

    [HttpPost("login")]
    public async Task<ActionResult<UserSignInDto>> SignIn([FromBody] UserCredentialRequest request)
    {
        var response = await _mediator.Send(new CheckUserSignInStatusQuery(request.Username, request.Password));

        if (response.UserSignInStatus == SignInStatus.Granted)
        {
            Dictionary<string, object> payloads = new();
            payloads.Add("UserId", "asdf");
            var token = _jwtTokenService.CreateJwtAccessToken(payloads);

			return Ok(token);
        }
        return Ok(response);
    }
}