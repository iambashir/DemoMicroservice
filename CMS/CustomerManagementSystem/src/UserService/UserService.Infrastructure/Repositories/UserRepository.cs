using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories;

public sealed class UserRepository(UserDbContext dbContext) : IUserRepository
{
    public Task<AppUser?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var normalized = userName.Trim().ToUpperInvariant();
        return dbContext.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == normalized, cancellationToken);
    }

    public Task<bool> UserNameExistsAsync(string userName, CancellationToken cancellationToken)
    {
        var normalized = userName.Trim().ToUpperInvariant();
        return dbContext.Users.AnyAsync(x => x.NormalizedUserName == normalized, cancellationToken);
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var normalized = email.Trim().ToUpperInvariant();
        return dbContext.Users.AnyAsync(x => x.NormalizedEmail == normalized, cancellationToken);
    }

    public async Task AddAsync(AppUser user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);
}
