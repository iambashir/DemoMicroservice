using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTOs;

public sealed class ChangePasswordRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string OldPassword { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
