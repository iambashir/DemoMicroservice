using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;
using CustomerService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;

    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<PagedCustomerResponse> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x =>
                x.CustomerName.Contains(keyword) ||
                x.ContactPerson.Contains(keyword) ||
                x.Mobile.Contains(keyword) ||
                x.Email.Contains(keyword) ||
                x.Address.Contains(keyword));
        }

        var total = await query.CountAsync(cancellationToken);
        var data = await query
            .OrderBy(x => x.CustomerId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CustomerDto
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                ContactPerson = x.ContactPerson,
                Mobile = x.Mobile,
                Email = x.Email,
                Address = x.Address,
                Status = x.Status
            })
            .ToListAsync(cancellationToken);

        return new PagedCustomerResponse
        {
            TotalRecords = total,
            Page = page,
            PageSize = pageSize,
            Data = data
        };
    }
}
