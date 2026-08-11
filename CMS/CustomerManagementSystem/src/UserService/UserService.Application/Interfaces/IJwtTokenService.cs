using UserService.Domain.Entities;

namespace UserService.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateAccessToken(AppUser user);
    string CreateRefreshToken();
    int ExpiresInSeconds { get; }
}
