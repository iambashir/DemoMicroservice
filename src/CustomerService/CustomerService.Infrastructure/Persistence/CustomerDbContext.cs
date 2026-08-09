using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Persistence;

public sealed class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(x => x.CustomerId);
            entity.Property(x => x.CustomerName).HasMaxLength(150).IsRequired();
            entity.Property(x => x.ContactPerson).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Mobile).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(250).IsRequired();
            entity.HasIndex(x => x.CustomerName);

            entity.HasData(
                new Customer { CustomerId = 1, CustomerName = "ABC Traders", ContactPerson = "Rahim Uddin", Mobile = "01812345678", Email = "abc@gmail.com", Address = "Dhaka", Status = true },
                new Customer { CustomerId = 2, CustomerName = "XYZ Corporation", ContactPerson = "Karim", Mobile = "01987654321", Email = "xyz@gmail.com", Address = "Chattogram", Status = true },
                new Customer { CustomerId = 3, CustomerName = "Delta Fashion", ContactPerson = "Nusrat Jahan", Mobile = "01711223344", Email = "delta@gmail.com", Address = "Gazipur", Status = true },
                new Customer { CustomerId = 4, CustomerName = "Green Agro", ContactPerson = "Hasan Mahmud", Mobile = "01655667788", Email = "green@gmail.com", Address = "Rajshahi", Status = true },
                new Customer { CustomerId = 5, CustomerName = "Metro Builders", ContactPerson = "Sajid Khan", Mobile = "01599887766", Email = "metro@gmail.com", Address = "Sylhet", Status = true },
                new Customer { CustomerId = 6, CustomerName = "Ocean Foods", ContactPerson = "Arif Hossain", Mobile = "01844556677", Email = "ocean@gmail.com", Address = "Khulna", Status = true },
                new Customer { CustomerId = 7, CustomerName = "Prime Logistics", ContactPerson = "Mim Akter", Mobile = "01933445566", Email = "prime@gmail.com", Address = "Narayanganj", Status = true },
                new Customer { CustomerId = 8, CustomerName = "Royal Motors", ContactPerson = "Tanvir Ahmed", Mobile = "01766778899", Email = "royal@gmail.com", Address = "Cumilla", Status = true },
                new Customer { CustomerId = 9, CustomerName = "Smart Electronics", ContactPerson = "Jahid Hasan", Mobile = "01622334455", Email = "smart@gmail.com", Address = "Dhaka", Status = true },
                new Customer { CustomerId = 10, CustomerName = "Sunrise Pharma", ContactPerson = "Runa Begum", Mobile = "01877889900", Email = "sunrise@gmail.com", Address = "Barishal", Status = true },
                new Customer { CustomerId = 11, CustomerName = "Tech Valley", ContactPerson = "Mehedi Islam", Mobile = "01900112233", Email = "tech@gmail.com", Address = "Dhaka", Status = true },
                new Customer { CustomerId = 12, CustomerName = "Urban Mart", ContactPerson = "Farhana Rahman", Mobile = "01788990011", Email = "urban@gmail.com", Address = "Rangpur", Status = true });
        });
    }
}
