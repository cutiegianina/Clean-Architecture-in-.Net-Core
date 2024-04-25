namespace Application.Models.IdentityServer;

public class JwtToken
{
	public string Token { get; set; }
	public DateTime IssuedAt { get; set; }
	public DateTime Expires { get; set; }
}