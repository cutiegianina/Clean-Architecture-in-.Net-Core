using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public sealed record CheckUserSignInStatusQuery(string Username, string Password) : IRequest<UserSignInDto>;

internal sealed class CheckUserSignInStatusQueryHandler : IRequestHandler<CheckUserSignInStatusQuery, UserSignInDto>
{
	private readonly IArgon2Hasher _argon2Hasher;
	private readonly IApplicationDbContext _context;

    public CheckUserSignInStatusQueryHandler(IArgon2Hasher argon2Hasher, IApplicationDbContext context) =>
        (_argon2Hasher, _context) = (argon2Hasher, context);

    public async Task<UserSignInDto> Handle(CheckUserSignInStatusQuery request, CancellationToken cancellationToken)
	{
		var user = await _context.User.FirstOrDefaultAsync(x => x.Username == request.Username);

		SignInStatus status = await CheckSignInStatus(user, request.Password);

		return new UserSignInDto()
		{
			User = user.Adapt<UserDto>(),
			UserSignInStatus = status
		};
	}

	private async Task<SignInStatus> CheckSignInStatus(User user, string password)
	{
		if (user is null)
			return SignInStatus.NotFound;
		else if (!await VerifyPasswordAsync(user, password))
			return SignInStatus.Invalid;

		return SignInStatus.Granted;
	}

	private async Task<bool> VerifyPasswordAsync(User user, string password) =>
		await _argon2Hasher.VerifyPasswordAsync(password, user.Password, user.Salt, user.Iterations);
}