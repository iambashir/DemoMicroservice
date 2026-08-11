namespace CustomerService.Application.DTOs;

public sealed class PagedCustomerResponse
{
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyList<CustomerDto> Data { get; set; } = Array.Empty<CustomerDto>();
}
