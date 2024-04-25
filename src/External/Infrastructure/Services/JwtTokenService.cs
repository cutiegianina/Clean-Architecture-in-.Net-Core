using Application.Common.Interfaces.Services;
using Application.Models.IdentityServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings    = jwtSettings.Value;
    }

    public string CreateJwtToken(DateTime issuedAt, DateTime expires, string secretKey, Dictionary<string ,object>? payload = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        var claims = new List<Claim>();
        
        if (payload  != null)
            foreach (var item in payload )
               claims.Add(new Claim(item.Key, item.Value.ToString()));

		var token = new JwtSecurityToken(
            issuer              : _jwtSettings.Issuer,
            audience            : _jwtSettings.Audience,
            claims              : claims,
            notBefore           : issuedAt,
            expires             : expires,
            signingCredentials  : credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public JwtToken CreateJwtAccessToken(Dictionary<string, object> payload = null)
    {
        var issuedAt    = DateTime.UtcNow;
        var expires     = issuedAt.AddMinutes(Convert.ToDouble(_jwtSettings.TokenExpirationInMinutes));
        var secretKey   = _jwtSettings.SecretKey;
		var token       = CreateJwtToken(issuedAt, expires, secretKey, payload);

        return new()
        {
            Token       = token,
            IssuedAt    = issuedAt,
            Expires     = expires,
        };
    }
}