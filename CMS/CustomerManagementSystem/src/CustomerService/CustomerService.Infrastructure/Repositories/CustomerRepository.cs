using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public sealed class CustomerRepository(CustomerDbContext dbContext) : ICustomerRepository
{
    public async Task<(int TotalRecords, IReadOnlyList<Customer> Customers)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken)
    {
        IQueryable<Customer> query = dbContext.Customers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var value = search.Trim();
            query = query.Where(x =>
                x.CustomerName.Contains(value) ||
                x.ContactPerson.Contains(value) ||
                x.Mobile.Contains(value) ||
                x.Email.Contains(value) ||
                x.Address.Contains(value));
        }

        var totalRecords = await query.CountAsync(cancellationToken);
        var customers = await query
            .OrderBy(x => x.CustomerId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (totalRecords, customers);
    }
}
