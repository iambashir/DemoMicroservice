using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services;

public sealed class UserAuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService) : IUserAuthService
{
    public async Task<(ApiMessageResponse Response, int StatusCode)> RegisterAsync(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.Password, request.ConfirmPassword, StringComparison.Ordinal))
        {
            return (Message(false, "Password and confirm password do not match."), 400);
        }

        if (await userRepository.UserNameExistsAsync(request.UserName, cancellationToken))
        {
            return (Message(false, "Username already exists."), 409);
        }

        if (await userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            return (Message(false, "Email already exists."), 409);
        }

        var password = passwordHasher.HashPassword(request.Password);
        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Mobile = request.Mobile.Trim(),
            UserName = request.UserName.Trim(),
            NormalizedEmail = request.Email.Trim().ToUpperInvariant(),
            NormalizedUserName = request.UserName.Trim().ToUpperInvariant(),
            PasswordHash = password.Hash,
            PasswordSalt = password.Salt
        };

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
        return (Message(true, "User registered successfully."), 201);
    }

    public async Task<(LoginResponse? Response, int StatusCode)> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserNameAsync(request.UserName, cancellationToken);
        if (user is null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return (null, 401);
        }

        return (new LoginResponse
        {
            Success = true,
            AccessToken = jwtTokenService.CreateAccessToken(user),
            RefreshToken = jwtTokenService.CreateRefreshToken(),
            UserName = user.UserName,
            FullName = user.FullName,
            ExpiresIn = jwtTokenService.ExpiresInSeconds
        }, 200);
    }

    public async Task<(ApiMessageResponse Response, int StatusCode)> ChangePasswordAsync(ChangePasswordRequest request, string? claimUserName, CancellationToken cancellationToken)
    {
        if (!string.Equals(claimUserName, request.UserName, StringComparison.OrdinalIgnoreCase))
        {
            return (Message(false, "Token user does not match request user."), 401);
        }

        if (!string.Equals(request.NewPassword, request.ConfirmPassword, StringComparison.Ordinal))
        {
            return (Message(false, "New password and confirm password do not match."), 400);
        }

        var user = await userRepository.GetByUserNameAsync(request.UserName, cancellationToken);
        if (user is null || !passwordHasher.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
        {
            return (Message(false, "Invalid old password."), 401);
        }

        var password = passwordHasher.HashPassword(request.NewPassword);
        user.PasswordHash = password.Hash;
        user.PasswordSalt = password.Salt;
        user.PasswordChangedAtUtc = DateTime.UtcNow;
        await userRepository.SaveChangesAsync(cancellationToken);
        return (Message(true, "Password changed successfully."), 200);
    }

    private static ApiMessageResponse Message(bool success, string message) => new()
    {
        Success = success,
        Message = message
    };
}
