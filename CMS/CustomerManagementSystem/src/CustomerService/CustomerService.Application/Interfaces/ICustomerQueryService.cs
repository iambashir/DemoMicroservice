using CustomerService.Application.DTOs;

namespace CustomerService.Application.Interfaces;

public interface ICustomerQueryService
{
    Task<PagedCustomerResponse> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken);
}
