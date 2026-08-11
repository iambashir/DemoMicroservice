using CustomerService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers;

[ApiController]
[Route("api/customer")]
public sealed class CustomerController(ICustomerQueryService customerQueryService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null, CancellationToken cancellationToken = default)
    {
        if (page < 1)
        {
            return BadRequest(new { success = false, message = "Page must be greater than or equal to 1." });
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new { success = false, message = "Page size must be between 1 and 100." });
        }

        var response = await customerQueryService.GetPagedAsync(page, pageSize, search, cancellationToken);
        return Ok(response);
    }
}
