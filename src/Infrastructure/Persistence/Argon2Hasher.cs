using Application.Common.Interfaces;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Persistence;

public class Argon2Hasher : IArgon2Hasher
{
	private const int Iteration = 20;

	public string GenerateSalt()
	{
		const int byteSize = 16;
		byte[] salt = new byte[byteSize];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(salt);

		return Convert.ToBase64String(salt);
	}

	public async Task<string> HashPasswordAsync(string password, string salt, int iterations)
	{
		const int byteSize = 16;

		using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

		argon2.Salt = Convert.FromBase64String(salt);
		argon2.DegreeOfParallelism = 8;
		argon2.MemorySize = 1024 * 1024;
		argon2.Iterations = iterations;

		var hashBytes = await argon2.GetBytesAsync(byteSize).ConfigureAwait(false);
		return Convert.ToBase64String(hashBytes);
	}

	public async Task<(string HashedPassword, string Salt, int Iterations)> HashPasswordAsync(string password)
	{
		string salt = GenerateSalt();
		return (await HashPasswordAsync(password, salt, Iteration), salt, Iteration);
	}

	public async Task<bool> VerifyPasswordAsync(string inputPassword, string hashedPassword, string salt, int iterations) =>
		FixedTimeEquals(
			Encoding.UTF8.GetBytes(await HashPasswordAsync(inputPassword, salt, iterations)),
			Encoding.UTF8.GetBytes(hashedPassword));

	public static bool FixedTimeEquals(byte[] left, byte[] right)
	{
		if (left.Length != right.Length)
			return false;

		var differences = 0;
		for (int i = 0; i < left.Length; i++)
			differences |= left[i] ^ right[i];

		return differences == 0;
	}
}