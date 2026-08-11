using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;

namespace CustomerService.Application.Services;

public sealed class CustomerQueryService
{
    private readonly ICustomerRepository _customers;

    public CustomerQueryService(ICustomerRepository customers)
    {
        _customers = customers;
    }

    public Task<PagedCustomerResponse> GetCustomersAsync(int page, int pageSize, string? search, CancellationToken cancellationToken)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 ? 10 : pageSize;
        pageSize = pageSize > 100 ? 100 : pageSize;

        return _customers.GetPagedAsync(page, pageSize, search, cancellationToken);
    }
}
