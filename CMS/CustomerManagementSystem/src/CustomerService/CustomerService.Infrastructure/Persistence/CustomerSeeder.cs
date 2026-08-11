using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Persistence;

public static class CustomerSeeder
{
    public static async Task SeedAsync(CustomerDbContext dbContext)
    {
        if (await dbContext.Customers.AnyAsync())
        {
            return;
        }

        var customers = new List<Customer>
        {
            Create("ABC Traders", "Rahim Uddin", "01812345678", "abc@gmail.com", "Motijheel, Dhaka", true),
            Create("Green Pharma", "Karim Ahmed", "01711223344", "green.pharma@gmail.com", "Dhanmondi, Dhaka", true),
            Create("Health Point", "Nusrat Jahan", "01944556677", "healthpoint@gmail.com", "Uttara, Dhaka", true),
            Create("City Diagnostics", "Mahmud Hasan", "01677889900", "citydx@gmail.com", "Mirpur, Dhaka", true),
            Create("Prime Medical Store", "Sadia Islam", "01599887766", "prime.med@gmail.com", "Banani, Dhaka", true),
            Create("Metro Surgical", "Arif Hossain", "01855667788", "metro.surgical@gmail.com", "Tejgaon, Dhaka", true),
            Create("Care Line", "Mizanur Rahman", "01766778899", "careline@gmail.com", "Wari, Dhaka", true),
            Create("North Star Clinic", "Shaila Akter", "01922334455", "northstar@gmail.com", "Gazipur", true),
            Create("Eastern Lab", "Rafiq Uddin", "01833445566", "easternlab@gmail.com", "Narayanganj", true),
            Create("Sunrise Healthcare", "Farhana Yasmin", "01744556677", "sunrise@gmail.com", "Savar", true),
            Create("Delta Medical", "Jahidul Islam", "01655667788", "delta.med@gmail.com", "Comilla", true),
            Create("Bengal Scientific", "Tariqul Islam", "01566778899", "bengal.science@gmail.com", "Chattogram", true),
            Create("Medicare Agency", "Sharmin Sultana", "01877889900", "medicare.agency@gmail.com", "Sylhet", true),
            Create("Popular Supply", "Nasir Khan", "01788990011", "popular.supply@gmail.com", "Rajshahi", true),
            Create("Labaid Corner", "Hasina Begum", "01999001122", "labaid.corner@gmail.com", "Khulna", true),
            Create("Safe Life", "Imran Hossain", "01810111213", "safelife@gmail.com", "Barishal", true),
            Create("Modern Care", "Ayesha Rahman", "01714151617", "moderncare@gmail.com", "Rangpur", true),
            Create("Central Pharmacy", "Babul Mia", "01618192021", "central.pharmacy@gmail.com", "Mymensingh", true),
            Create("Vision Medical", "Labib Chowdhury", "01522232425", "vision.med@gmail.com", "Badda, Dhaka", true),
            Create("Trust Diagnostics", "Rumana Akter", "01826272829", "trustdx@gmail.com", "Mohakhali, Dhaka", true),
            Create("Wellness Depot", "Sabbir Ahmed", "01730313233", "wellness.depot@gmail.com", "Keraniganj", true),
            Create("Unity Hospital Supply", "Naimur Rahman", "01934353637", "unity.supply@gmail.com", "Jatrabari, Dhaka", true),
            Create("New Life Clinic", "Tania Sultana", "01838394041", "newlife@gmail.com", "Narsingdi", true),
            Create("Medi Fast", "Foysal Kabir", "01742434445", "medifast@gmail.com", "Tangail", false),
            Create("Care Plus", "Anika Hossain", "01646474849", "careplus@gmail.com", "Kushtia", true),
            Create("Bio Tech Agency", "Omar Faruk", "01550515253", "biotech@gmail.com", "Bogura", true)
        };

        await dbContext.Customers.AddRangeAsync(customers);
        await dbContext.SaveChangesAsync();
    }

    private static Customer Create(string customerName, string contactPerson, string mobile, string email, string address, bool status)
    {
        return new Customer
        {
            CustomerName = customerName,
            ContactPerson = contactPerson,
            Mobile = mobile,
            Email = email,
            Address = address,
            Status = status
        };
    }
}
