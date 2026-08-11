using UserService.Application.DTOs;

namespace UserService.Application.Interfaces;

public interface IUserAuthService
{
    Task<(ApiMessageResponse Response, int StatusCode)> RegisterAsync(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task<(LoginResponse? Response, int StatusCode)> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<(ApiMessageResponse Response, int StatusCode)> ChangePasswordAsync(ChangePasswordRequest request, string? claimUserName, CancellationToken cancellationToken);
}
