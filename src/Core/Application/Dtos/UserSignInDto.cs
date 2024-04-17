using Application.Common.Enums;

namespace Application.Dtos;

public class UserSignInDto
{
	public UserDto? User { get; set; }
	public SignInStatus? UserSignInStatus { get; set; }
}
