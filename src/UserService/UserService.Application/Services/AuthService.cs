using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services;

public sealed class AuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository users, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<MessageResponse> RegisterAsync(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        if (await _users.ExistsByUserNameAsync(request.UserName, cancellationToken))
        {
            return MessageResponse.Fail("Username already exists.");
        }

        if (await _users.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            return MessageResponse.Fail("Email already exists.");
        }

        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Mobile = request.Mobile.Trim(),
            UserName = request.UserName.Trim(),
            PasswordHash = _passwordHasher.Hash(request.Password)
        };

        await _users.AddAsync(user, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);

        return MessageResponse.Ok("User registered successfully.");
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.GetByUserNameAsync(request.UserName, cancellationToken);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        var token = _jwtTokenGenerator.Generate(user);
        return new AuthResponse
        {
            Success = true,
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            UserName = user.UserName,
            FullName = user.FullName,
            ExpiresIn = token.ExpiresIn
        };
    }

    public async Task<MessageResponse> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.GetByUserNameAsync(request.UserName, cancellationToken);
        if (user is null || !_passwordHasher.Verify(request.OldPassword, user.PasswordHash))
        {
            return MessageResponse.Fail("Old password is incorrect.");
        }

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        await _users.SaveChangesAsync(cancellationToken);

        return MessageResponse.Ok("Password changed successfully.");
    }
}
