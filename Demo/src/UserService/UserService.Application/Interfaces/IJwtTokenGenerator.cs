using UserService.Domain.Entities;

namespace UserService.Application.Interfaces;

public interface IJwtTokenGenerator
{
    (string AccessToken, string RefreshToken, int ExpiresIn) Generate(AppUser user);
}
