using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands;

public record CreateUserCommand(UserDto User) : IRequest<(int, SignInResult)>;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (int, SignInResult)>
{
    private readonly IApplicationDbContext _context;
    private readonly IArgon2Hasher _argon2Hasher;

    public CreateUserCommandHandler(IApplicationDbContext context, IArgon2Hasher argon2Hasher) =>
        (_context, _argon2Hasher) = (context, argon2Hasher);

    public async Task<(int, SignInResult)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
        var user = request.User.Adapt<User>();

        bool checkUsernameIfExists= await _context.User.AnyAsync(user => user.Username == request.User.Username);
        bool checkEmailIfExists = await _context.User.AnyAsync(user => user.Email == request.User.Email);

        if (checkUsernameIfExists)
            return (0, SignInResult.UsernameAlreadyExists);

        if (checkEmailIfExists)
            return (0, SignInResult.EmailAlreadyExists);

        await GeneratePasswordAsync(user);
        await _context.User.AddAsync(user, cancellationToken);

        var result = await _context.SaveChangesAsync(cancellationToken);
        return (result, SignInResult.Success);
	}

    private async Task GeneratePasswordAsync(User user)
    {
	    (string hashedPassword, string salt, int iterations) = await _argon2Hasher.HashPasswordAsync(user.Password);
	    user.Password = hashedPassword;
	    user.Salt = salt;
	    user.Iterations = iterations;
	}
}