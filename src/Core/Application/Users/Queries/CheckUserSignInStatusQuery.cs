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

		var isVerified = await _argon2Hasher.VerifyPasswordAsync(
			request.Password,
			user.Password,
			user.Salt,
			user.Iterations);

		SignInStatus status = CheckSignInStatus(user, isVerified);

		return new UserSignInDto()
		{
			User = user.Adapt<UserDto>(),
			UserSignInStatus = status
		};
	}

	private static SignInStatus CheckSignInStatus(User user, bool isVerified)
	{
		if (user is null)
			return SignInStatus.NotFound;
		else if (!isVerified)
			return SignInStatus.Invalid;

		return SignInStatus.Granted;
	}
}