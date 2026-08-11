using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;

namespace CustomerService.Application.Services;

public sealed class CustomerQueryService(ICustomerRepository customerRepository) : ICustomerQueryService
{
    public async Task<PagedCustomerResponse> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken)
    {
        var result = await customerRepository.GetPagedAsync(page, pageSize, search, cancellationToken);
        return new PagedCustomerResponse
        {
            TotalRecords = result.TotalRecords,
            Page = page,
            PageSize = pageSize,
            Data = result.Customers.Select(x => new CustomerDto
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                ContactPerson = x.ContactPerson,
                Mobile = x.Mobile,
                Email = x.Email,
                Address = x.Address,
                Status = x.Status
            }).ToList()
        };
    }
}
