namespace UserService.Application.DTOs;

public sealed class AuthResponse
{
    public bool Success { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}
