namespace UserService.Application.DTOs;

public sealed class MessageResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public static MessageResponse Ok(string message) => new() { Success = true, Message = message };
    public static MessageResponse Fail(string message) => new() { Success = false, Message = message };
}
