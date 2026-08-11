using CustomerService.Application.DTOs;

namespace CustomerService.Application.Interfaces;

public interface ICustomerRepository
{
    Task<PagedCustomerResponse> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken);
}
