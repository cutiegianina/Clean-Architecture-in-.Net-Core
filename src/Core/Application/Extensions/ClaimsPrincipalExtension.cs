using Domain.Constants;
using System.Security.Claims;

namespace Application.Extensions;

public static class ClaimsPrincipalExtension
{
	public static string? GetUserIdClaim(this ClaimsPrincipal claims) =>
		claims.Claims.FirstOrDefault(claim => claim.Type == JwtTokenClaims.UserId)?.Value;
}