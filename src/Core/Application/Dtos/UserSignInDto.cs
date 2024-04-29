using Application.Common.Enums;
using Application.Models.IdentityServer;

namespace Application.Dtos;

public class UserSignInDto
{
	public UserDto? User { get; set; }
	public SignInStatus? UserSignInStatus { get; set; }
	public JwtToken? Token { get; set; }
}