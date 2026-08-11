using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController(IUserAuthService userAuthService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await userAuthService.LoginAsync(request, cancellationToken);
        if (result.Response is null)
        {
            return Unauthorized(new { success = false, message = "Invalid username or password." });
        }

        return StatusCode(result.StatusCode, result.Response);
    }
}
