using CustomerService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers;

[ApiController]
[Authorize]
public sealed class CustomerController : ControllerBase
{
    private readonly CustomerQueryService _customerQueryService;

    public CustomerController(CustomerQueryService customerQueryService)
    {
        _customerQueryService = customerQueryService;
    }

    [HttpGet("api/customer")]
    public async Task<IActionResult> GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken cancellationToken = default)
    {
        var customers = await _customerQueryService.GetCustomersAsync(page, pageSize, search, cancellationToken);
        return Ok(customers);
    }
}
