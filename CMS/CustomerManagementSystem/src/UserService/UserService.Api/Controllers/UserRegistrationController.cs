using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UserRegistrationController(IUserAuthService userAuthService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var result = await userAuthService.RegisterAsync(request, cancellationToken);
        return StatusCode(result.StatusCode, result.Response);
    }
}
