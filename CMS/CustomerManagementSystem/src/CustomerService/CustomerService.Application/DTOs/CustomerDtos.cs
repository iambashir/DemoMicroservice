namespace CustomerService.Application.DTOs;

public sealed class CustomerDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool Status { get; set; }
}

public sealed class PagedCustomerResponse
{
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyList<CustomerDto> Data { get; set; } = Array.Empty<CustomerDto>();
}
