using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/changePassword")]
public sealed class ChangePasswordController(IUserAuthService userAuthService) : ControllerBase
{
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var claimUserName = User.FindFirstValue("userName") ?? User.Identity?.Name;
        var result = await userAuthService.ChangePasswordAsync(request, claimUserName, cancellationToken);
        return StatusCode(result.StatusCode, result.Response);
    }
}
