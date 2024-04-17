namespace Application.Common.Enums;

public enum SignInStatus
{
	Granted								= 1,
	Invalid								= 2,
	NotFound							= 3,
	PasswordExpired						= 4,
	Deactivated							= 5,
	TwoWayFactorAuthenticationFailed	= 6
}