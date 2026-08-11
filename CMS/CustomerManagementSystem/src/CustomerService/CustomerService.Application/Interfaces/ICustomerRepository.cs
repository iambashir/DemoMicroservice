using CustomerService.Domain.Entities;

namespace CustomerService.Application.Interfaces;

public interface ICustomerRepository
{
    Task<(int TotalRecords, IReadOnlyList<Customer> Customers)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken);
}
