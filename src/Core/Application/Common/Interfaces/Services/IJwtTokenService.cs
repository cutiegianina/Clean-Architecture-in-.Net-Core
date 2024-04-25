using Application.Models.IdentityServer;

namespace Application.Common.Interfaces.Services;

public interface IJwtTokenService
{
	string CreateJwtToken(DateTime issuedAt, DateTime expires, string secretKey, Dictionary<string, object> payload = null);
	JwtToken CreateJwtAccessToken(Dictionary<string, object> payload = null);
}	