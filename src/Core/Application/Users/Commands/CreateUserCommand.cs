using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Users.Commands;

public record CreateUserCommand(UserDto User) : IRequest<(int, SignInResult)>;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (int, SignInResult)>
{
    private readonly IUnitOfWork<User> _unitOfWork;
    private readonly IArgon2Hasher _argon2Hasher;

    public CreateUserCommandHandler(IUnitOfWork<User> unitOfWork, IArgon2Hasher argon2Hasher) =>
        (_unitOfWork, _argon2Hasher) = (unitOfWork, argon2Hasher);

    public async Task<(int, SignInResult)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
        var user = request.User.Adapt<User>();
        if (user is not null)
            return (0, SignInResult.UsernameAlreadyExists);

        await GeneratePasswordAsync(user);
        await _unitOfWork.AddAsync(user, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
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