namespace Application.Common.Interfaces;

public  interface IArgon2Hasher
{
	string GenerateSalt();
	Task<string> HashPasswordAsync(string password, string salt, int iterations);
	Task<(string HashedPassword, string Salt, int Iterations)> HashPasswordAsync(string password);
	Task<bool> VerifyPasswordAsync(string inputPassword, string hashedPassword, string salt, int iterations);
}