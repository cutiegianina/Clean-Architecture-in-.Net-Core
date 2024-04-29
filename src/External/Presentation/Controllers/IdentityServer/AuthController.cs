using Application.Common.Enums;
using Application.Common.Interfaces.Services;
using Application.Dtos;
using Application.Users.Queries;
using Domain.Constants;
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
			Dictionary<string, object> payloads = new()
			{
				{ JwtTokenClaims.UserId, response.User.Id }
			};
			var token = _jwtTokenService.CreateJwtAccessToken(payloads);

            response.Token = token;
        }
        return Ok(response);
    }

/*    private void SetJwtAccessTokenCookie(JwtToken token)
    {
        CookieOptions jwtTokenCookieOptions = new();

        if (token is not null)
            jwtTokenCookieOptions.Expires = token.Expires;
        else
            jwtTokenCookieOptions.Expires = DateTime.UtcNow.AddMinutes(10);


        jwtTokenCookieOptions.HttpOnly = true;
        jwtTokenCookieOptions.Path = "/";
        jwtTokenCookieOptions.Secure = true;

        Response.Cookies.Append("JWT", token.Token, jwtTokenCookieOptions);
    }*/
}